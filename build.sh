projectName='Yozian.MyGrid'

dotnet publish src/$projectName/$projectName.csproj \
    --force \
    -c Release \
    -o "bin/publish"

cp bin/publish/$projectName.dll nuget/lib/netstandard2.0/
cp bin/publish/$projectName.pdb nuget/lib/netstandard2.0/
