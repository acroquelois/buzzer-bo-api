# API Buzzer

Ce projet contient l'api l'api .NET du projet Buzzer

## Lancer l'api

> docker-compose up --build -d

Cette commande lancera l'api sur le port 80 de votre machine,une base de donnée MySql, une serveur SWAGGER sur le port 4444 et un serveur de log Seq sur le port 3333. Les ports sont modifiable dans le fichier **docker-compose.yml**

## Utilisation de l'api

Pour utiliser les endpoints présent dans l'api il est nécessaire de créer un utilisater via le endpoint __api/user/createUser__ (voir le Swagger) puis de se logger via le endpoint __/api/auth/login__ ou via l'interface.

## L'environnment docker

### Image Mysql

Sur le projet la base de donnée est une base de donnée MySql sa configuration est modifiable via **docker-compose.yml**, dans la configuration actuel seulement l'api .NET est capable de s'y connecter grâce à la restriction d'adresse ip.

### Image Swagger

L'image swagger nous permet d'avoir une documentation très clair pour tout les endpoints mis à disposition par l'api. Le swagger est, dans la configuration actuel, exposé sur le port 4444 et est basé sur le documentation de l'api herbergé. Cette documentation est modifiable en changant la viriable d'environnement URL.

### Image Seq

Seq permet de créer un serveur de log qui sauvegarde toutes les actions faites par les utilisateurs de l'api. Les logs sont stockées sur un espace du disque sur votre machine, elles sont persistantent. Le serveur est exposé sur le port 3333 et est également modifiable

### Image .Net

L'image .Net contient tout le build du projet, c'est l'api, elle expose sur le port 80.