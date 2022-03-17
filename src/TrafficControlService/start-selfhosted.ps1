dapr run `
    --app-id trafficcontrolservice `
    --app-port 6001 `
    --dapr-http-port 3600 `
    --dapr-grpc-port 6100 `
    --config ../dapr/config/config.yaml `
    --components-path ../dapr/components