FROM node:18-alpine AS base
WORKDIR /app
COPY package*.json ./

FROM base AS test
RUN npm ci
COPY . .
CMD ["npm", "run", "test"]

FROM base AS final
RUN npm ci
COPY . .
RUN npm run build
EXPOSE 5173
CMD ["npm", "run", "preview", "--", "--host", "0.0.0.0", "--port", "5173"]