using Microsoft.Azure.CognitiveServices.Vision.ComputerVision;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision.Models;

string edpoint = "https://svitvison.cognitiveservices.azure.com/";
string key = "3b16134e01174427a23721a9e23cfe2f";
string descriptionUrl = "https://gdb.rferl.org/01000000-0a00-0242-7800-08db59f55466_w1023_r1_s.jpg";
ImageAnalysis analysis = await GetImageAnalisisAsync(key, edpoint, descriptionUrl);
GetImageDescription(analysis);
GetFacesInfo(analysis);
Console.ReadLine();

ComputerVisionClient CreateVisionClient(string key, string endpoint)
{
    ApiKeyServiceClientCredentials clientCredentials = new ApiKeyServiceClientCredentials(key);
    ComputerVisionClient visionClient = new ComputerVisionClient(clientCredentials)
    {
        Endpoint = endpoint
    };
    return visionClient;
}

async Task<ImageAnalysis> GetImageAnalisisAsync(string key, string endpoint, string url)
{
    ComputerVisionClient visionClient = CreateVisionClient(key, endpoint);
    IList<VisualFeatureTypes?> featureTypes = Enum.GetValues(typeof(VisualFeatureTypes)).OfType<VisualFeatureTypes?>().ToList();
    ImageAnalysis imageAnalysis = await visionClient.AnalyzeImageAsync(url, featureTypes);

    return imageAnalysis;
}

void GetImageDescription(ImageAnalysis analysis)
{
    Console.WriteLine($"Image Descriptions=>");
    foreach (var caption in analysis.Description.Captions)
    {
        Console.WriteLine($"{caption.Text} with confidence: {caption.Confidence}");
    }
}

void GetFacesInfo(ImageAnalysis analysis)
{
    Console.WriteLine("Recognides faces=>");
    foreach (var face in analysis.Faces)
    {
        Console.WriteLine($"Genser: {face.Gender}");
        Console.WriteLine($"Age: {face.Age}");
        Console.WriteLine($"Top: {face.FaceRectangle.Top}, Left: {face.FaceRectangle.Left}, Widht: {face.FaceRectangle.Width}," +
            $"Height: {face.FaceRectangle.Height}");
        Console.WriteLine("------------------------");
    }
}