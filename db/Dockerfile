FROM mcr.microsoft.com/mssql/server

ENV ACCEPT_EULA=Y

ENV SA_PASSWORD=Cloud9_district

COPY ./setup.sql /
COPY ./entrypoint.sh /

ENTRYPOINT [ "/bin/bash", "entrypoint.sh" ]

CMD [ "/opt/mssql/bin/sqlservr" ]