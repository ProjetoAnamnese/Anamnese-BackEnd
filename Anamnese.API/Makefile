default:
	@echo "Available commands:"
	@echo "make bc              - Create container"
	@echo "make run             - Initialize Dotnet Server"
	@echo "make stop            - Stop containers"
	@echo "make sc              - Start Containers"
	@echo "make delete          - Delete container"


bc:
	docker run --name=mysql_container -e MYSQL_ROOT_PASSWORD=1234 -p 3306:3306 -d mysql:latest
	@echo "Containers running"

run:
	dotnet run

sc:
	docker start mysql_container
	@echo "Container running"

stop:
	docker stop mysql_container
	@echo "Container stopped"

delete:
	docker rm mysql_container
	@echo "Container deleted"

