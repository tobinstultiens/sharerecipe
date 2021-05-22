#!/bin/sh
sed "s/KEYCLOAK_HOST/keycloak.$(minikube ip).nip.io/" keycloak-ingress.yaml | kubectl create -f -