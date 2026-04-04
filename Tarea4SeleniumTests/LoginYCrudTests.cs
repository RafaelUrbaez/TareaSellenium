using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Edge;
using System;
using System.Threading;

namespace Tarea4SeleniumTests
{
    public class LoginYCrudTests
    {
        private IWebDriver _driver;

        [SetUp]
        public void Setup()
        {
            _driver = new EdgeDriver();
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            _driver.Manage().Window.Maximize();
        }

        [Test]
        public void Prueba_FlujoCompleto_Login_Y_Crud()
        {
            // --- 1. LOGIN ---
            _driver.Navigate().GoToUrl("http://localhost:5068/Account/Login");
            _driver.FindElement(By.Id("Username")).SendKeys("rafael_admin");
            _driver.FindElement(By.Id("Password")).SendKeys("1234");
            Thread.Sleep(1000); 
            _driver.FindElement(By.CssSelector("button[type='submit']")).Click();
            Thread.Sleep(2000); 

            // --- 2. CREAR UN VIDEOJUEGO ---
            _driver.FindElement(By.LinkText("Registrar Nuevo Juego")).Click();
            Thread.Sleep(1000);
            
            _driver.FindElement(By.Id("Titulo")).SendKeys("Marvel Rivals");
            _driver.FindElement(By.Id("Genero")).SendKeys("Shooter");
            _driver.FindElement(By.Id("Plataforma")).SendKeys("PC");
            _driver.FindElement(By.Id("Precio")).SendKeys("0");
            Thread.Sleep(1000); 
            
            _driver.FindElement(By.CssSelector("input[type='submit']")).Click();
            Thread.Sleep(2000); 

            // --- 3. EDITAR UN VIDEOJUEGO ---
            _driver.FindElement(By.LinkText("Editar")).Click();
            Thread.Sleep(1000);

            var inputPrecio = _driver.FindElement(By.Id("Precio"));
            inputPrecio.Clear();
            inputPrecio.SendKeys("1500");
            Thread.Sleep(1000);
            
            _driver.FindElement(By.CssSelector("input[type='submit']")).Click();
            Thread.Sleep(2000); 

            // --- 4. ELIMINAR UN VIDEOJUEGO ---
            _driver.FindElement(By.LinkText("Eliminar")).Click();
            Thread.Sleep(1000); 
            
            _driver.FindElement(By.CssSelector("input[type='submit']")).Click();
            Thread.Sleep(2000); 

            // Comprobación final actualizada para NUnit 4
            Assert.That(_driver.Url.Contains("/Videojuegos"), Is.True, "El flujo falló.");
        }

        [TearDown]
        public void TearDown()
        {
            _driver.Quit();
            _driver.Dispose();
        }
    }
}