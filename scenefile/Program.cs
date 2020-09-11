using System;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace scenefile
{
    class Program
    {
        static async Task<int> Main(string[] args)
        {
            const String DataHubEndpoint = "<Your datahub endpoint>";
            const String BearerToken = "<Your JWT token>";
            const String SceneId = "<Your Scene id>";
            const String FileName = "<Your picture file name>";

            SceneFileRequest req = new SceneFileRequest();
            req.ContentType = "image/png";
            req.Description = "SceneFile3";
            req.Name = "ViseoImgry3";
            req.SceneId = SceneId;
            req.Type = "farm-mask";

            // Imgs pour un champs vers Jean Maçon, Guiche, France
            // https://www.google.fr/maps/@43.4973974,-1.1906858,245m/data=!3m1!1e3
            // https://www.dronegenuity.com/orthomosaic-maps-explained/

            using (HttpClient client = new HttpClient())
            {

                String reqJson = SerializeSceneFileRequest.ToJson(req);

                var body = new StringContent(reqJson, Encoding.UTF8, "application/json");

                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + BearerToken);

                using (HttpResponseMessage res = await client.PostAsync(DataHubEndpoint, body))
                {
                    string jsonMessage = await res.Content.ReadAsStringAsync();
                    try
                    {
                        SceneFileResponse apiRes = SceneFileResponse.FromJson(jsonMessage);

                        // Generate image
                        Stream fs = GenerateImg.GenerateFromLocalFile(FileName);

                        // Send it to BlobStorage
                        BlobStorageSingleton.UploadSas(apiRes.UploadSasUrl, fs);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("ERROR");
                        Console.WriteLine("REQUEST : " + SerializeSceneFileRequest.ToJson(req));
                        Console.WriteLine("RESPONSE : " + jsonMessage);
                        throw e;
                    }
                }
            }


            return 0;

        }
    }
}
