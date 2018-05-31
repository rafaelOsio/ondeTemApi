FROM microsoft/aspnetcore:2.0.0
MAINTAINER Rafael Osio
COPY . /app
WORKDIR /app
ENV ASPNETCORE_URLS http://*:5000
EXPOSE 5000
ENTRYPOINT ["dotnet", "Api.dll"]