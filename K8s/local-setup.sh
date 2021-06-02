#!/bin/sh
#minikube config set driver kvm2
minikube start --cpus=8 --memory=16384
#minikube addons enable ingress
# Create ShareRecipe
kubectl apply -f sharerecipe-namespace.yaml
kubectl config set-context sharerecipe --namespace=sharerecipe --cluster=minikube --user=minikube
kubectl config use-context sharerecipe
kubectl apply -f secrets
# Install istio if not installed
if ! command -v istioctl &> /dev/null
then
    echo "istio could not be found"
		exit 0;
fi
# Configure Istio
istioctl install -y
kubectl label namespace sharerecipe istio-injection=enabled
# Setup metallb
kubectl apply -f https://raw.githubusercontent.com/metallb/metallb/v0.9.5/manifests/namespace.yaml
kubectl apply -f https://raw.githubusercontent.com/metallb/metallb/v0.9.5/manifests/metallb.yaml
kubectl create secret generic -n metallb-system memberlist --from-literal=secretkey="$(openssl rand -base64 128)"
kubectl apply -f metallb
# Setup local mounts
minikube ssh -- sudo mkdir /mnt/data
minikube ssh -- sudo mkdir /mnt/data/sharerecipe-profile-db
minikube ssh -- sudo mkdir /mnt/data/sharerecipe-follower-db
minikube ssh -- sudo mkdir /mnt/data/sharerecipe-kweet-db
minikube ssh -- sudo mkdir /mnt/data/keycloak-db
minikube ssh -- sudo mkdir /mnt/data/rabbit-store
minikube ssh -- sudo mkdir /mnt/data/rabbit-store-1
minikube ssh -- sudo mkdir /mnt/data/rabbit-store-2
minikube ssh -- sudo mkdir /mnt/data/rabbit-store-3
minikube ssh -- sudo mkdir /mnt/data/eventstore
minikube ssh -- sudo chown -R 10001:0 /mnt/data
# Setup Storage
kubectl apply -f storage/sharerecipe-storage.yaml
kubectl apply -f storage/
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
