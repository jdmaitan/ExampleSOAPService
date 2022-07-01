using FiscalInfoWebService.Data;
using FiscalInfoWebService.Models.InputModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace FiscalInfoWebService.Services
{
    public class DataService
    {
        private FiscalInfoDbContext dbContext;

        public DataService()
        {
            dbContext = new FiscalInfoDbContext();
        }

        public async Task<SpecificMessage> GetResponseMessage(int specificMessageCode)
        {
            return await dbContext.SpecificMessages.Where(m => m.ID == specificMessageCode)
                                                   .Include(m => m.GeneralMessage)
                                                   .SingleOrDefaultAsync();
        }

        public async Task<bool> GetServiceStatus()
        {
            return await dbContext.ServiceStatus.Where(s => s.ID == 1)
                                                .Select(s => s.Running)
                                                .SingleOrDefaultAsync();
        }

        public async Task<List<string>> GetValidRIFPrefixes()
        {
            return await dbContext.ValidRIFLetters.Select(v => v.Letter)
                                                  .ToListAsync();
        }

        public async Task<int> CreateTaxpayerAsync(FiscalInfoInput input)
        {
            Taxpayer taxpayer = new Taxpayer
            {
                RIF = input.RIF,
                BusinessName = input.BusinessName,
                Address = input.Address
            };

            dbContext.Taxpayers.Add(taxpayer);
            await dbContext.SaveChangesAsync();
            return taxpayer.ID;
        }

        public async Task<Taxpayer> GetTaxpayerAsync(string RIF)
        {
            return await dbContext.Taxpayers
                 .Where(t => t.RIF == RIF)
                 .SingleOrDefaultAsync();
        }

        public async Task<int> UpdateCustomerAsync(FiscalInfoInput input)
        {
            Taxpayer taxpayer = await GetTaxpayerAsync(input.RIF);

            if (taxpayer == null)
            {
                throw new Exception($"Couldn't find taxpayer with RIF = {input.RIF}");
            }

            taxpayer.BusinessName = input.BusinessName;
            taxpayer.Address = input.Address;

            await dbContext.SaveChangesAsync();

            return taxpayer.ID;
        }

        public async Task DeleteTaxpayerAsync(string RIF)
        {
            Taxpayer taxpayer = await GetTaxpayerAsync(RIF);

            if (taxpayer == null)
            {
                throw new Exception($"Couldn't find taxpayer with RIF = {RIF}");
            }

            dbContext.Taxpayers.Remove(taxpayer);

            await dbContext.SaveChangesAsync();
        }
    }
}