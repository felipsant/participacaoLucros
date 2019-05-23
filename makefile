#!/bin/sh
build:
	@docker build -t participacao . 
run:
	@docker run -p 7071:80 --name participacao participacao 
start:
	@docker start participacao 
stop:
	@docker stop participacao
clean:
	@docker rm participacao
logs:
	@docker logs participacao 
ps:
	@docker ps
publish:
	