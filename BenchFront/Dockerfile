FROM node:latest as node
RUN mkdir -p /app
WORKDIR /app
COPY BenchFront/. .

RUN npm install
RUN npm run build --prod

FROM nginx:latest
COPY BenchFront/nginx.conf BenchFront/etc/nginx/nginx.conf
COPY --from=node /app/dist/BenchFront /usr/share/nginx/html
