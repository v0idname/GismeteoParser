# Gismeteo Parser
Система из нескольких проектов для парсинга html данных о погоде с gismeteo.ru для популярных городов на ближайшие 10 дней.

## Technologies Used
- .NET Framework 4.7
- IoC (Autofac)
- Entity Framework Core 3.1, MySQL 5.7.36
- ASP.NET (MVC + Web API)

## Architecture
| Проект      | Описание |
| ----------- | ----------- |
| GismeteoParser.Data | Проект с описанием используемых типов данных и контекста БД |
| GismeteoParser.Grabber | Консольный проект для парсинга данных с сайта и их записи в БД |
| GismeteoParser.WebService | Web-API проект, предоставляющий клиентам информацию о погоде из БД |
| GismeteoParser.ServiceClient | Интерфейс для работы с Web API, используемый клиентами |
| GismeteoParser.ConsoleClient | Консольный клиент |
| GismeteoParser.ConsoleClient | Web клиент |

## Usage
1. В проекте WebService в файле Web.config указать строку подключения к БД.
2. Запустить Grabber.
3. Запустить WebService.
4. Запустить любой из клиентов.

(*) всё это удобнее делать через опцию Multiple Startup Projects в Visual Stidio
