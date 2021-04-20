FROM mcr.microsoft.com/mssql/server

ENV ACCEPT_EULA=Y

ENV SA_PASSWORD=Cloud9-district

COPY ./setup.sql /
COPY ./entrypoint.sh /
EXPOSE 1433

ENTRYPOINT [ "/bin/bash", "entrypoint.sh" ]

CMD [ "/opt/mssql/bin/sqlservr" ]

#CMD /bin/bash entrypoint.sh
