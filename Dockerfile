FROM bitnami/dotnet-sdk:6 as builder

COPY src /app
RUN dotnet publish -c release


FROM bitnami/dotnet-sdk:6 as runner

COPY --from=builder /app/bin/release/net6.0/publish/ /entrypoint
RUN dotnet tool install ilspycmd -g && echo 'export PATH="$PATH:/app/.dotnet/tools"' >> ~/.bash_profile

COPY LICENSE README.md /

ENTRYPOINT ["/entrypoint/entrypoint"]
