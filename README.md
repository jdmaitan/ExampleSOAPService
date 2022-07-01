# ExampleSOAPService
This is an example of a SOAP web service developed in ASP.NET with WCF and C#

This solution gives the users the posibility of doing create, update or read operations over their fiscal data which is being saved in a remote database. Also, it provides a way to verify their RIF (the ID number given to every taxpayer in Venezuela by the main tax agency of the country, SENIAT). Because the input of this fiscal data sometimes would be a pain when processing multiple taxpayers, i was asked to develop a RIF PDF file parser, so it would obtain all the neccesary data from the taxpayers without risk of wrong input data.

I developed a larger version of this solution using MySQL for a production environment some time ago using the old ASP.NET framework; when NET Core was still something new. I applied the template method design pattern to the controllers behind the SOAP methods, so that whenever I needed to add another type of validation or processing, it would make the code easier to maintain and scale, thus ensuring that I applied SOLID principles (specifically SRP and DRY).

One way to improve this code would be to apply depency injection to the private used services in every class.

If you want to use this code as template, take note that for this example i used a SQLite database, so you can run the project locally without the hassle of installing a bigger DB server. You can change the DB provider and use the same logic for accesing the data thanks to the use of entity framework in the DataService class.
