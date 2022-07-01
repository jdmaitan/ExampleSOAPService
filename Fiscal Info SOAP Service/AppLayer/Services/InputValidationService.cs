using System.Text.RegularExpressions;

namespace FiscalInfoWebService.Services
{
    public class InputValidationService
    {
        public static readonly int RIF_LENGTH = 10;
        public static readonly int BUSINESS_NAME_LENGTH = 100;
        public static readonly int ADDRESS_LENGTH = 500;

        MessageService messageService;

        public InputValidationService()
        {
            messageService = new MessageService();
        }

        public bool LengthIsLessThan(string input, int maxLength)
        {
            return input.Length <= maxLength;
        }

        public bool LengthIs(string input, int length)
        {
            return input.Length == length;
        }

        public bool HasEmptySpaces(string input)
        {
            return input.Contains(" ");
        }

        public bool HasOnlyNumbers(string input)
        {
            return Regex.IsMatch(input, @"^[0-9]+$");
        }

        public bool RIFHasAValidCheckDigit(string input)
        {
            char rifLetter = input[0];

            int firstMultiplier = 0;

            switch (rifLetter)
            {
                case 'V':
                    firstMultiplier = 1;
                    break;

                case 'E':
                    firstMultiplier = 2;
                    break;

                case 'J':
                    firstMultiplier = 3;
                    break;

                case 'G':
                    firstMultiplier = 5;
                    break;

                case 'C':
                    firstMultiplier = 3;
                    break;

                default:
                    break;
            }

            int rifValueWithoutDV = int.Parse(input.Substring(1, input.Length - 2));

            int[] multiplicands = new int[] { 4, 3, 2, 7, 6, 5, 4, 3, 2 };

            int[] multipliers = new int[]
            {
                firstMultiplier,
                rifValueWithoutDV/10000000,
                (rifValueWithoutDV%10000000)/1000000,
                (rifValueWithoutDV%1000000)/100000,
                (rifValueWithoutDV%100000)/10000,
                (rifValueWithoutDV%10000)/1000,
                (rifValueWithoutDV%1000)/100,
                (rifValueWithoutDV%100)/10,
                (rifValueWithoutDV%10),
            };

            int Summatory = 0;

            for (int i = 0; i < multiplicands.Length; i++)
            {
                Summatory += multiplicands[i] * multipliers[i];
            }

            int calculatedCheckDigit = 11 - (Summatory % 11);

            if (calculatedCheckDigit > 9)
            {
                calculatedCheckDigit = 0;
            }

            int givenRIFCheckDigit = int.Parse(input.Substring(input.Length - 1));

            if (givenRIFCheckDigit == calculatedCheckDigit)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool IsAValidRIFAsync(string input, ref string responseMessage)
        {
            if (string.IsNullOrEmpty(input))
            {
                responseMessage = messageService.GetResponseMessage(401);
                return false;
            }

            if (HasEmptySpaces(input))
            {
                responseMessage = messageService.GetResponseMessage(402);
                return false;
            }

            if (!LengthIs(input, RIF_LENGTH))
            {
                responseMessage = messageService.GetResponseMessage(403);
                return false;
            }

            if (!HasOnlyNumbers(input.Substring(1, 9)))
            {
                responseMessage = messageService.GetResponseMessage(404);
                return false;
            }

            if (!RIFHasAValidCheckDigit(input))
            {
                responseMessage = messageService.GetResponseMessage(410);
                return false;
            }

            return true;
        }

        public bool IsAValidBusinessName(string input, ref string responseMessage)
        {
            if (string.IsNullOrEmpty(input))
            {
                responseMessage = messageService.GetResponseMessage(406);
                return false;
            }

            if (!LengthIsLessThan(input, BUSINESS_NAME_LENGTH))
            {
                responseMessage = messageService.GetResponseMessage(407);
                return false;
            }

            return true;
        }

        public bool IsAValidAddress(string input, ref string responseMessage)
        {
            if (string.IsNullOrEmpty(input))
            {
                responseMessage = messageService.GetResponseMessage(411);
                return false;
            }

            if (!LengthIsLessThan(input, ADDRESS_LENGTH))
            {
                responseMessage = messageService.GetResponseMessage(412);
                return false;
            }

            return true;
        }

        public bool IsAValidBase64String(string input, ref string responseMessage)
        {
            if (string.IsNullOrEmpty(input))
            {
                responseMessage = messageService.GetResponseMessage(408);
                return false;
            }

            if (HasEmptySpaces(input))
            {
                responseMessage = messageService.GetResponseMessage(409);
                return false;
            }

            return true;
        }
    }
}