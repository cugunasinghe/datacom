# datacom
Datacom Timesheet Processor

Reference Meterial:
https://restsharp.dev/intro.html#introduction
https://joshclose.github.io/CsvHelper/getting-started/
https://developers.datacomdirectaccess.co.nz/api/documentation/

API Feedback:
Most of the API calls are simple and easy to deal with. However, in my opinion a lot can improve on the documentation side. 
For a example, it would be nice to have a mock of a payload(request) for each API call.
Furthermore some of the API are lacking fileration capabilities.
  ex: Company cannot be filtered using a Company Code directly from the API.
      PayRun list cannot be filtered directly using the start and end dates.
      It would be nice to have the grouping capabilities within the Timesheet api endpoint.
That being said, these imperfections might not be there in the actual production environmet. 
However should be there in the development (demo environment) since thats the api that acts as a sandbox for the developmers
