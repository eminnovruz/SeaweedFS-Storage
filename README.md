# SeaweedFS .NET Integration

![SeaweedFS Logo](https://www.google.com/url?sa=i&url=https%3A%2F%2Fgithub.com%2Fseaweedfs%2Fseaweedfs&psig=AOvVaw3RRbEjr3ixgH72LUFhEvap&ust=1722323939899000&source=images&cd=vfe&opi=89978449&ved=0CBEQjRxqFwoTCIDFruTay4cDFQAAAAAdAAAAABAR)

## Overview
This project integrates SeaweedFS with a .NET application. SeaweedFS is a distributed file system designed for high scalability and efficiency.

## Setup

### SeaweedFS Installation
1. **Download SeaweedFS**:
   - Download the latest release from the [SeaweedFS GitHub releases page](https://github.com/chrislusf/seaweedfs/releases).

2. **Run SeaweedFS**:
   - Start the SeaweedFS master server:
     ```bash
     ./weed master -mdir=/tmp/seaweedfs
     ```
   - Start the SeaweedFS volume server:
     ```bash
     ./weed volume -dir=/tmp/seaweedfs -max=5
     ```
   - Optionally, start the SeaweedFS filer server:
     ```bash
     ./weed filer -filer.dir=/tmp/seaweedfs
     ```

3. **Access SeaweedFS Web Interface**:
   - Navigate to `http://localhost:9333/` in your browser.

## .NET Application Setup

### Adding SeaweedFS Integration

1. **Install HttpClient**:
   - Ensure you have `HttpClient` for making HTTP requests.

2. **API Endpoint to Upload Files**:
   - Create an endpoint to handle file uploads (example provided in the ASP.NET Web API section).

3. **API Endpoint to Retrieve Files**:
   ```csharp
   [Route("api/[controller]")]
   [ApiController]
   public class FilesController : ControllerBase
   {
       private readonly string _seaweedFsUrl = "http://localhost:9333";

       [HttpGet("{filePath}")]
       public async Task<IActionResult> GetFile(string filePath)
       {
           string fileUrl = $"{_seaweedFsUrl}/{filePath}";

           using (HttpClient client = new HttpClient())
           {
               HttpResponseMessage response = await client.GetAsync(fileUrl);

               if (response.IsSuccessStatusCode)
               {
                   var fileContent = await response.Content.ReadAsByteArrayAsync();
                   var contentType = response.Content.Headers.ContentType.ToString();
                   return File(fileContent, contentType, filePath);
               }
               else
               {
                   return NotFound();
               }
           }
       }
   }
   
## Commands for SeaweedFS Interaction

- **Upload File**:
  To upload a file to SeaweedFS, use the following `curl` command. Replace `yourfile.txt` with the path to the file you want to upload:
  ```bash
  curl -F "file=@yourfile.txt" http://localhost:9333/upload

  
