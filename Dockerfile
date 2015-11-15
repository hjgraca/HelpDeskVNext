FROM microsoft/aspnet:1.0.0-beta8-coreclr

COPY . /app/
WORKDIR /app
RUN ["dnu", "restore"]

EXPOSE 5000
ENTRYPOINT ["dnx", "-p", "./src/HelpDeskVNext/project.json", "web"]
