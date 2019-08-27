rmdir /S /Q bin
rmdir /S /Q obj
dotnet restore
npm install
npm install -g bower
bower install
pause