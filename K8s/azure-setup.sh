#!/bin/sh
# Create ShareRecipe
kubectl config set-context sharerecipe-admin --namespace=sharerecipe --cluster=sharerecipe --user=clusterAdmin_sharerecipe_sharerecipe
kubectl config use-context sharerecipe-admin
kubectl apply -f sharerecipe-namespace.yaml
kubectl apply -f secrets
# Install istio if not installed
if ! command -v istioctl &> /dev/null
then
    echo "istio could not be found"
		exit 0;
fi
# Configure Istio
istioctl install -y
#istioctl operator init
#istioctl profile dump default
kubectl label namespace sharerecipe istio-injection=enabled
# Setup metallb
#kubectl apply -f https://raw.githubusercontent.com/metallb/metallb/v0.9.5/manifests/namespace.yaml
#kubectl apply -f https://raw.githubusercontent.com/metallb/metallb/v0.9.5/manifests/metallb.yaml
#kubectl create secret generic -n metallb-system memberlist --from-literal=secretkey="$(openssl rand -base64 128)"
#kubectl apply -f metallb
# Setup Storage
kubectl apply -f azure
# Install rabbitmq
#kubectl apply -f https://github.com/rabbitmq/cluster-operator/releases/latest/download/cluster-operator.yml
#kubectl apply -f RabbitMQ
helm install rabbitmq bitnami/rabbitmq --set auth.password=guest --set auth.username=guest
# Setup databases
kubectl apply -f databases
# Setup Gateway
kubectl apply -f gateway
# Setup services
kubectl apply -f services
