.PHONY: help build-backend build-frontend build-all up down logs clean deploy-k8s delete-k8s

help:
	@echo "Portfolio Application - Make Commands"
	@echo "======================================"
	@echo "build-backend    - Build backend Docker image"
	@echo "build-frontend   - Build frontend Docker image"
	@echo "build-all        - Build both Docker images"
	@echo "up               - Start application with Docker Compose"
	@echo "down             - Stop application"
	@echo "logs             - View application logs"
	@echo "clean            - Remove containers and images"
	@echo "deploy-k8s       - Deploy to Kubernetes"
	@echo "delete-k8s       - Delete from Kubernetes"

build-backend:
	@echo "Building backend Docker image..."
	docker build -t portfolio-backend:latest ./backend

build-frontend:
	@echo "Building frontend Docker image..."
	docker build -t portfolio-frontend:latest ./frontend

build-all: build-backend build-frontend
	@echo "All images built successfully!"

up:
	@echo "Starting application..."
	docker-compose up -d
	@echo "Application started!"
	@echo "Frontend: http://localhost:4200"
	@echo "Backend: http://localhost:5000"

down:
	@echo "Stopping application..."
	docker-compose down

logs:
	docker-compose logs -f

clean:
	@echo "Cleaning up..."
	docker-compose down -v
	docker rmi portfolio-backend:latest portfolio-frontend:latest 2>/dev/null || true
	@echo "Cleanup complete!"

deploy-k8s: build-all
	@echo "Deploying to Kubernetes..."
	kubectl apply -f k8s/backend-deployment.yaml
	kubectl apply -f k8s/frontend-deployment.yaml
	kubectl apply -f k8s/ingress.yaml
	@echo "Deployment complete!"
	@echo "Check status: kubectl get pods"

delete-k8s:
	@echo "Deleting from Kubernetes..."
	kubectl delete -f k8s/
	@echo "Deletion complete!"

dev-backend:
	@echo "Starting backend in development mode..."
	cd backend && dotnet run

dev-frontend:
	@echo "Starting frontend in development mode..."
	cd frontend && npm start
