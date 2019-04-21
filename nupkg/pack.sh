#!/bin/bash

nuget_repo=""
push=""
apikey=""
build="yes"
workdir=$(cd $(dirname $0); pwd)
slnPath="${workdir}/sln"
srcPath="${workdir}/src"

while [[ $# -gt 0 ]]; do
  case "$1" in
    -r | --repo )
      nuget_repo="$2"; shift 2;;
    -p | --push )
       push="yes"; shift 2;;
    --ship-build )
       build=''; shift 2;;
    -k | --apikey )
       apikey="$2" shift 2;;
    -h | --help )
        usage; exit 1 ;;
    *)
        echo "Unknown option $1"
        usage; exit 2 ;;    
  esac
done

projects=(
  "Surging.Core.ApiGateWay"
  "Surging.Core.Caching"
  "Surging.Core.Codec.MessagePack"
  "Surging.Core.Codec.ProtoBuffer"
  "Surging.Core.Common"
  "Surging.Core.Consul"
  "Surging.Core.CPlatform"
  "Surging.Core.DotNetty"
  "Surging.Core.EventBusKafka"
  "Surging.Core.EventBusRabbitMQ"
  "Surging.Core.KestrelHttpServer"
  "Surging.Core.Log4net"
  "Surging.Core.NLog"
  "Surging.Core.Protocol.Http"
  "Surging.Core.Protocol.WS"
  "Surging.Core.ProxyGenerator"
  "Surging.Core.ServiceHosting"
  "Surging.Core.Swagger"
  "Surging.Core.System"
  "Surging.Core.Zookeeper"
  "Surging.Core.Domain"
  "Surging.Core.Schedule"
  "Surging.Core.AutoMapper"
  "Surging.Core.Dapper"
  "WebSocketCore"
)

if [[ $build ]]; then
  cd ${slnPath}
  dotnet restore Surging.sln
  for project in ${projects}
  do
    projectFolder="${srcPath}/${project}"
    cd ${projectFolder}
    rm -fr "$projectFolder/bin/Release"
    dotnet msbuild /p:Configuration=Release /p:SourceLinkCreate=true
    dotnet msbuild /t:pack /p:Configuration=Release /p:SourceLinkCreate=true
    $projectPackPath="${projectFolder}/bin/Release/${project}.*.nupkg"
    mv $projectPackPath $workdir
  done
fi

if [[ $push ]]; then {
    if ([string]::IsNullOrEmpty($apikey)){
        echo "未设置nuget仓库的APIKEY"
		exit 1
	}
	dotnet nuget push *.nupkg -s $repo -k $apikey
}