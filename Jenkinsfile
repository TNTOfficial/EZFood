pipeline {
    agent { label 'ezfooddo' }

    stages {
        
        stage('Git checkout') {
            steps {
                checkout scm
            }
        }
     
        stage('Build Docker Image') {
            steps {
                sh 'docker build . -t ezfood-api/ezfood-api-dev'
                // sh 'NODE_ENV=devuction npm run build'
            }
        }
    
        stage('Run Docker Image') {
            steps {
                sh 'echo "Stopping Running ezfoodApi_dev container"'
                sh 'docker stop ezfoodApi_dev || true'
		            sh 'echo "Stopped Running ezfoodApi_dev container"'
                sh 'echo "Removing Running ezfoodApi_dev container"'
                sh 'docker rm ezfoodApi_dev || true'
		            sh 'echo "Removed Running ezfoodApi_dev container"'
                sh 'echo "Adding Volume ezfoodApiVolume_dev"'
                sh 'docker volume create ezfoodApiVolume_dev || true'
		            sh 'echo "Added Volume ezfoodApiVolume_dev"'
                sh 'docker run -d -p 5000:8080 -v ezfoodApiVolume_dev:/app/uploads --restart=always --name ezfoodApi_dev ezfood-api/ezfood-api-dev'
            }
        }
    }
}
