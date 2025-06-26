FROM nginx:alpine

COPY ./api/ ./result/api
COPY ./idp/ ./result/idp
COPY ./notes/ ./result/notes
