# Ticketing.Web

A ticketing system to demo Kubernetes

## Setup

```bash
docker build -t denhamparry/ticketing.web:local .
docker run --rm -it -p 3001:80 --name ticketing.web --network=ticketing-network -d denhamparry/ticketing.web:local
...
```

## Start

```bash
$ docker start ticketing.web:local
ticketing.web
```

* [Home Page](http://localhost:3001/)

## Delete

```bash
$ docker stop ticketing.web
ticketing.web
```