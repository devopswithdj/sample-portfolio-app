# Portfolio Application

A modern, full-stack portfolio application built with Angular and .NET, containerized with Docker and ready for Kubernetes deployment.

## 🏗️ Architecture

### Frontend
- **Framework**: Angular 17 (Standalone Components)
- **Styling**: Custom CSS with distinctive design aesthetic
- **Features**:
  - Responsive hero section with animated gradient backgrounds
  - Dynamic project showcase with hover effects
  - Animated skills display with proficiency bars
  - Contact form with validation
  - Smooth scrolling and micro-interactions

### Backend
- **Framework**: ASP.NET Core 8 (Minimal APIs)
- **Features**:
  - RESTful API endpoints
  - CORS configuration
  - Health check endpoint
  - Swagger/OpenAPI documentation
  - In-memory data store (easily replaceable with database)

### Deployment
- **Containerization**: Docker
- **Orchestration**: Kubernetes
- **Web Server**: Nginx (for Angular frontend)

## 📁 Project Structure

```
portfolio-app/
├── backend/
│   ├── Program.cs                    # Main API application
│   ├── PortfolioAPI.csproj          # .NET project file
│   └── Dockerfile                    # Backend container image
├── frontend/
│   ├── src/
│   │   ├── app/
│   │   │   ├── components/
│   │   │   │   ├── hero/            # Hero section component
│   │   │   │   ├── projects/        # Projects showcase component
│   │   │   │   ├── skills/          # Skills display component
│   │   │   │   └── contact/         # Contact form component
│   │   │   ├── services/
│   │   │   │   └── api.service.ts   # API client service
│   │   │   ├── app.component.*      # Root component
│   │   │   └── app.routes.ts        # Application routes
│   │   ├── styles.css               # Global styles
│   │   ├── index.html               # Main HTML
│   │   └── main.ts                  # Bootstrap file
│   ├── angular.json                 # Angular configuration
│   ├── package.json                 # Node dependencies
│   ├── Dockerfile                   # Frontend container image
│   └── nginx.conf                   # Nginx configuration
├── k8s/
│   ├── backend-deployment.yaml      # Backend K8s deployment
│   ├── frontend-deployment.yaml     # Frontend K8s deployment
│   └── ingress.yaml                 # Ingress configuration
└── docker-compose.yml               # Local development setup
```

## 🚀 Quick Start

### Prerequisites
- Docker and Docker Compose
- Node.js 18+ (for local development)
- .NET 8 SDK (for local development)
- Kubernetes cluster (for K8s deployment)

### Local Development with Docker Compose

1. **Clone and navigate to the project**:
   ```bash
   cd portfolio-app
   ```

2. **Build and run with Docker Compose**:
   ```bash
   docker-compose up --build
   ```

3. **Access the application**:
   - Frontend: http://localhost:4200
   - Backend API: http://localhost:5000/api
   - Swagger UI: http://localhost:5000/swagger

### Local Development (without Docker)

#### Backend
```bash
cd backend
dotnet restore
dotnet run
```
The API will be available at http://localhost:5000

#### Frontend
```bash
cd frontend
npm install
npm start
```
The application will be available at http://localhost:4200

## 🐳 Docker Build

### Build Backend Image
```bash
cd backend
docker build -t portfolio-backend:latest .
```

### Build Frontend Image
```bash
cd frontend
docker build -t portfolio-frontend:latest .
```

### Run Containers Manually
```bash
# Backend
docker run -d -p 5000:8080 --name backend portfolio-backend:latest

# Frontend
docker run -d -p 4200:80 --name frontend portfolio-frontend:latest
```

## ☸️ Kubernetes Deployment

### Prerequisites
- Running Kubernetes cluster (Minikube, Kind, or cloud provider)
- kubectl configured
- Nginx Ingress Controller installed

### Deploy to Kubernetes

1. **Build and load images** (for local clusters like Minikube):
   ```bash
   # Build images
   docker build -t portfolio-backend:latest ./backend
   docker build -t portfolio-frontend:latest ./frontend
   
   #Push Images to Registry
   docker push portfolio-backend:latest
   docker push portfolio-frontend:latest

   # For Minikube
   minikube image load portfolio-backend:latest
   minikube image load portfolio-frontend:latest
   ```

2. **Deploy the application**:
   ```bash
   kubectl apply -f k8s/backend-deployment.yaml
   kubectl apply -f k8s/frontend-deployment.yaml
   kubectl apply -f k8s/ingress.yaml
   ```
   ```
   IF Image is pulling back off with authentication error, you can use below command to create secret and attach in your deployment yaml.

   kubectl create secret docker-registry regcred --docker-server=https://index.docker.io/v1/ --docker-username=<your username> --docker-password=<your password> --docker-email=<your email>

   in deployment yaml uncomment ImagePullSecrets, once the secret is created.

   ```

3. **Verify deployment**:
   ```bash
   kubectl get pods
   kubectl get services
   kubectl get ingress
   ```

4. **Access the application**:
   - For LoadBalancer service: Get external IP with `kubectl get svc frontend`
   - For Ingress: Add `portfolio.local` to your `/etc/hosts` file pointing to your cluster IP
      Add <svc external IP> portfolio.local
      On Windows: edit C:\Windows\System32\drivers\etc\hosts
      Add-Content -Path "C:\Windows\System32\drivers\etc\hosts" -Value "172.18.255.200 portfolio.local"
   - For Minikube: `minikube service frontend --url`

### Scale the Application
```bash
# Scale backend
kubectl scale deployment portfolio-backend --replicas=3

# Scale frontend
kubectl scale deployment portfolio-frontend --replicas=3
```

### View Logs
```bash
# Backend logs
kubectl logs -f deployment/portfolio-backend

# Frontend logs
kubectl logs -f deployment/portfolio-frontend
```

### Delete Deployment
```bash
kubectl delete -f k8s/
```

## 🛠️ Configuration

### Backend Configuration
Edit `backend/Program.cs` to:
- Add database connections
- Configure additional services
- Add authentication/authorization
- Modify CORS policies

### Frontend Configuration
Edit `frontend/src/app/services/api.service.ts` to change the API URL:
```typescript
private apiUrl = 'http://your-api-url/api';
```

### Kubernetes Resources
Resource limits are configured in deployment files:
- Backend: 256Mi-512Mi RAM, 250m-500m CPU
- Frontend: 128Mi-256Mi RAM, 100m-200m CPU

Adjust based on your needs in the respective YAML files.

## 🎨 Customization

### Design
The application features a distinctive design with:
- Playfair Display font for headings (serif, elegant)
- DM Sans for body text (sans-serif, modern)
- Custom color palette (purple/gold/red)
- Animated gradient backgrounds
- Smooth transitions and micro-interactions

Customize colors in `frontend/src/styles.css`:
```css
:root {
  --color-primary: #2d1b4e;
  --color-secondary: #d4af37;
  --color-accent: #ff6b6b;
  /* ... more variables */
}
```

### Content
Update the data in `backend/Program.cs`:
- Projects list
- Skills list
- Experience entries

## 📊 API Endpoints

- `GET /api/health` - Health check
- `GET /api/projects` - Get all projects
- `GET /api/projects/{id}` - Get project by ID
- `GET /api/skills` - Get all skills
- `GET /api/experience` - Get work experience
- `POST /api/contact` - Submit contact form

## 🔐 Production Considerations

Before deploying to production:

1. **Security**:
   - Add authentication/authorization
   - Configure HTTPS/TLS
   - Set up proper CORS policies
   - Add rate limiting
   - Implement input validation

2. **Database**:
   - Replace in-memory data with a real database
   - Add database migrations
   - Implement proper data persistence

3. **Monitoring**:
   - Add application insights/logging
   - Set up health checks
   - Configure alerts

4. **Performance**:
   - Enable caching
   - Add CDN for static assets
   - Optimize images
   - Enable compression

5. **CI/CD**:
   - Set up automated builds
   - Implement automated testing
   - Configure deployment pipelines

## 📝 License

This project is open source and available under the MIT License.

## 🤝 Contributing

Contributions, issues, and feature requests are welcome!

## 👤 Author

Your Name - [GitHub](https://github.com/yourusername)
