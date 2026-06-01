# Estágio de Build
FROM node:22-alpine AS build
WORKDIR /app

# Copia package.json e package-lock.json
COPY package*.json ./
RUN npm ci

# Copia o código da aplicação
COPY . ./

# Build da aplicação
RUN npm run build

# Estágio de Runtime
FROM nginx:alpine
WORKDIR /usr/share/nginx/html

# Remove configuração padrão do nginx
RUN rm -rf /etc/nginx/conf.d/default.conf

# Copia arquivo de configuração do nginx
COPY nginx.conf /etc/nginx/conf.d/default.conf

# Copia os arquivos compilados do estágio anterior
COPY --from=build /app/dist .

EXPOSE 80

CMD ["nginx", "-g", "daemon off;"]
