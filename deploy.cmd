nuget pack
nuget push *.nupkg -Source https://api.nuget.org/v3/index.json
move *.nupkg Releases
