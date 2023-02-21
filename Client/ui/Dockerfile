FROM node:lts-alpine as build-stage

ARG VUE_APP_API_URL
ENV VUE_APP_API_URL ${VUE_APP_API_URL}

WORKDIR /app

COPY package*.json ./
RUN npm install  --legacy-peer-deps

COPY . .
RUN nmp run build

# production stage
FROM nginx:stable-alpine as production-stage

COPY --from=build-stage /app/dist /usr/share/nginx/html
EXPOSE 8080
CMD ["nginx", "-g", "daemon off;"]