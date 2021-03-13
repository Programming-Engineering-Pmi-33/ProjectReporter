CREATE DATABASE IF NOT EXISTS project_reporter;
DROP USER IF EXISTS 'developer'@'%';
CREATE USER 'developer'@'%' IDENTIFIED BY 'password';
GRANT ALL PRIVILEGES ON project_reporter.* TO 'developer'@'%';
FLUSH PRIVILEGES;
USE project_reporter;