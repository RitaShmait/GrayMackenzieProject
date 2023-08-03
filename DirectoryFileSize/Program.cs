

int GetFilesNumberFromFolderSubFolder(string directory)
{
    var files = new List<string>(Directory.GetFiles(directory));
    var folders = new List<string>(Directory.GetDirectories(directory));
    return files.Count + folders.Select(GetFilesNumberFromFolderSubFolder).ToList().Sum();
}

while (true)
{
    
    Console.WriteLine("Enter starting point");
    var folder = Console.ReadLine();
    if (string.IsNullOrWhiteSpace(folder))
    {
        Console.WriteLine("Empty String is not allowed");
    }

    var files = GetFilesNumberFromFolderSubFolder(folder!);
    Console.WriteLine($"Number of files under {folder} = {files}");
}