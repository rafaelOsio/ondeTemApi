FROM microsoft/aspnetcore:2.0.0
MAINTAINER Rafael Osio
COPY . /app/ondeTem
WORKDIR /app/ondeTem
ENV ASPNETCORE_URLS http://*:4174
EXPOSE 4174
ENTRYPOINT ["dotnet", "ondeTem.WebApi.dll"]
