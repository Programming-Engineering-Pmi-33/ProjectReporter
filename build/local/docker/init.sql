CREATE DATABASE IF NOT EXISTS project_reporter CHARACTER SET koi8u_bin COLLATE koi8u_general_ci;
DROP USER IF EXISTS 'developer'@'%';
CREATE USER 'developer'@'%' IDENTIFIED BY 'password';
GRANT ALL PRIVILEGES ON project_reporter.* TO 'developer'@'%';
FLUSH PRIVILEGES;
USE project_reporter;