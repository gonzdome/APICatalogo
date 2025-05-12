global using Newtonsoft.Json;
global using Microsoft.AspNetCore.Mvc;
global using Microsoft.AspNetCore.Identity;
global using Microsoft.EntityFrameworkCore;
global using Microsoft.IdentityModel.Tokens;
global using Microsoft.AspNetCore.Authentication.JwtBearer;

global using APICatalogo.DTOs;
global using APICatalogo.Models;
global using APICatalogo.Filters;
global using APICatalogo.Context;
global using APICatalogo.Logging;
global using APICatalogo.Services;
global using APICatalogo.Validation;
global using APICatalogo.Extensions;
global using APICatalogo.Repositories;
global using APICatalogo.Helpers.Paginate;
global using APICatalogo.Helpers.Paginate.Filters;
global using APICatalogo.Repositories.Interfaces;
global using APICatalogo.Services.Interfaces;

global using APICatalogo.Models.ViewModels;
global using APICatalogo.Models.ViewModels.Categories;
global using APICatalogo.Models.ViewModels.Products;

global using System.Text;
global using System.Text.Json;
global using System.Text.Json.Serialization;
global using System.Collections.ObjectModel;
global using System.ComponentModel.DataAnnotations;
global using System.ComponentModel.DataAnnotations.Schema;
