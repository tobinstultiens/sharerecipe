#!/bin/sh
minikube start
# Create ShareRecipe
kubectl config set-context sharerecipe --namespace=sharerecipe --cluster=minikube --user=minikube
kubectl config use-context sharerecipe
kubectl apply -f sharerecipe-namespace.yaml
# Install istio if not installed
if ! command -v istioctl &> /dev/null
then
    echo "istio could not be found"
		paru -S istio
fi
# Configure Istio
istioctl install -y
kubectl label namespace sharerecipe istio-injection=enabled
# Setup local mounts
minikube ssh -- sudo mkdir /mnt/data
minikube ssh -- sudo mkdir /mnt/data/sharerecipe-profile-db
minikube ssh -- sudo mkdir /mnt/data/sharerecipe-follow-db
minikube ssh -- sudo mkdir /mnt/data/sharerecipe-kweet-db
minikube ssh -- sudo mkdir /mnt/data/rabbit-store
minikube ssh -- sudo mkdir /mnt/data/rabbit-store-1
minikube ssh -- sudo mkdir /mnt/data/rabbit-store-2
minikube ssh -- sudo mkdir /mnt/data/rabbit-store-3
minikube ssh -- sudo chown -R 10001:0 /mnt/data
# Setup Storage
kubectl apply -f storage/sharerecipe-storage.yaml
kubectl apply -f storage/
# Install rabbitmq
kubectl apply -f https://github.com/rabbitmq/cluster-operator/releases/latest/download/cluster-operator.yml
kubectl apply -f RabbitMQ
# Setup databases
kubectl apply -f databases
# Setup services
kubectl apply -f services

