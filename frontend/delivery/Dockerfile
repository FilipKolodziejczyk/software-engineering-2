FROM node:18-alpine AS base
WORKDIR /app
COPY package*.json ./

FROM base AS test
RUN npm ci
COPY . .
CMD ["npm", "run", "test"]

FROM base AS final
ARG VITE_API_BASE_URL
ARG VITE_CONFIG_PATH="vite.config.ts"
ENV VITE_API_BASE_URL $VITE_API_BASE_URL
ENV VITE_CONFIG_PATH $VITE_CONFIG_PATH
RUN npm ci
COPY . .
RUN npm run build
EXPOSE 5173
CMD ["sh", "-c", "npm run preview -- --host 0.0.0.0 --port 5173 --config ${VITE_CONFIG_PATH}"]