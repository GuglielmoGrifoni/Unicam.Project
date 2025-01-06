using System.Net;
using System.Text;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.Graph.Models.ExternalConnectors;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Unicam.Project.Application.Abstractions.Services;
using Unicam.Project.Application.Factories;
using Unicam.Project.Application.Options;
using Unicam.Project.Application.Services;
using Unicam.Project.Models.Context;
using Unicam.Project.Models.Repository;
using System.Text.Json;
using Unicam.Project.Web.Extensions;
using Unicam.Project.Application.Extensions;
using Unicam.Project.Models.Extensions;

var builder = WebApplication.CreateBuilder(args);

//SERVIZI

builder.Services
    .AddWebServices(builder.Configuration)
    .AddApplicationServices(builder.Configuration)
    .AddModelServices(builder.Configuration);

var app = builder.Build();

//MIDDLEWARE

app.AddWebMiddleware();

app.Run();
