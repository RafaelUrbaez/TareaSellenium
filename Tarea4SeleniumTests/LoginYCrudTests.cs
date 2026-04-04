using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Edge;
using System;
using System.IO;
using System.Threading;

namespace Tarea4SeleniumTests
{
    public class CasosDePruebaCompletos
    {
        private IWebDriver _driver;
        private string _rutaCapturas = Path.Combine(TestContext.CurrentContext.WorkDirectory, "Capturas");

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            if (!Directory.Exists(_rutaCapturas)) Directory.CreateDirectory(_rutaCapturas);
        }

        [SetUp]
        public void Setup()
        {
            _driver = new EdgeDriver();
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            _driver.Manage().Window.Maximize();
        }

        private void TomarCaptura(string nombreArchivo)
        {
            Screenshot foto = ((ITakesScreenshot)_driver).GetScreenshot();
            foto.SaveAsFile(Path.Combine(_rutaCapturas, $"{nombreArchivo}.png"));
        }

        #region HISTORIA 1: LOGIN
        [Test, Order(1)]
        public void H1_Login_Limite_CamposVacios()
        {
            _driver.Navigate().GoToUrl("http://localhost:5068/Account/Login");
            Thread.Sleep(3000); 
            _driver.FindElement(By.CssSelector("button[type='submit']")).Click();
            Thread.Sleep(3000);
            TomarCaptura("H1_Limite");
            Assert.That(_driver.Url.Contains("/Videojuegos"), Is.False, "Dejó entrar sin datos.");
        }

        [Test, Order(2)]
        public void H1_Login_Negativo_ClaveFalsa()
        {
            _driver.Navigate().GoToUrl("http://localhost:5068/Account/Login");
            Thread.Sleep(2000);
            _driver.FindElement(By.Id("Username")).SendKeys("rafael_admin");
            Thread.Sleep(2500);
            _driver.FindElement(By.Id("Password")).SendKeys("claveEquivocada");
            Thread.Sleep(3000);
            _driver.FindElement(By.CssSelector("button[type='submit']")).Click();
            Thread.Sleep(3000);
            TomarCaptura("H1_Negativo");
            Assert.That(_driver.Url.Contains("/Videojuegos"), Is.False);
        }

        [Test, Order(3)]
        public void H1_Login_Feliz_Correcto()
        {
            _driver.Navigate().GoToUrl("http://localhost:5068/Account/Login");
            Thread.Sleep(2000);
            _driver.FindElement(By.Id("Username")).SendKeys("rafael_admin");
            Thread.Sleep(2500);
            _driver.FindElement(By.Id("Password")).SendKeys("1234");
            Thread.Sleep(3000);
            _driver.FindElement(By.CssSelector("button[type='submit']")).Click();
            Thread.Sleep(3000);
            TomarCaptura("H1_Feliz");
            Assert.That(_driver.Url.Contains("/Videojuegos"), Is.True);
        }
        #endregion

        #region HISTORIA 3: READ (LISTADO)
        [Test, Order(4)]
        public void H3_Read_Negativo_IdInvalido()
        {
            LoginRutaBase();
            Thread.Sleep(3000);
            _driver.Navigate().GoToUrl("http://localhost:5068/Videojuegos/Details/9999");
            Thread.Sleep(3000);
            TomarCaptura("H3_Negativo");
            Assert.That(_driver.PageSource.Contains("Valorant"), Is.False);
        }

        [Test, Order(5)]
        public void H3_Read_Limite_CargaRapida()
        {
            LoginRutaBase();
            Thread.Sleep(2000);
            _driver.Navigate().Refresh();
            Thread.Sleep(3000);
            TomarCaptura("H3_Limite");
            var tabla = _driver.FindElement(By.TagName("table"));
            Assert.That(tabla.Displayed, Is.True);
        }

        [Test, Order(6)]
        public void H3_Read_Feliz_CargaCatalogo()
        {
            LoginRutaBase();
            Thread.Sleep(3000);
            TomarCaptura("H3_Feliz");
            Assert.That(_driver.PageSource.Contains("Catálogo de Videojuegos"), Is.True);
        }
        #endregion

        #region HISTORIA 2: CREATE
        [Test, Order(7)]
        public void H2_Create_Negativo_CamposVacios()
        {
            LoginRutaBase();
            Thread.Sleep(2000);
            _driver.FindElement(By.LinkText("Registrar Nuevo Juego")).Click();
            Thread.Sleep(3000);
            _driver.FindElement(By.CssSelector("input[type='submit']")).Click();
            Thread.Sleep(3000);
            TomarCaptura("H2_Negativo");
            Assert.That(_driver.Url.Contains("/Create"), Is.True);
        }

        [Test, Order(8)]
        public void H2_Create_Limite_PrecioGigante()
        {
            LoginRutaBase();
            Thread.Sleep(2000);
            _driver.FindElement(By.LinkText("Registrar Nuevo Juego")).Click();
            Thread.Sleep(2000);
            _driver.FindElement(By.Id("Titulo")).SendKeys("Juego Premium");
            _driver.FindElement(By.Id("Genero")).SendKeys("RPG");
            _driver.FindElement(By.Id("Plataforma")).SendKeys("PC");
            _driver.FindElement(By.Id("Precio")).SendKeys("9999999.99");
            Thread.Sleep(3000);
            _driver.FindElement(By.CssSelector("input[type='submit']")).Click();
            Thread.Sleep(3000);
            TomarCaptura("H2_Limite");
            Assert.That(_driver.Url.Contains("/Create"), Is.True, "Debería chocar por el rango de precio.");
        }

        [Test, Order(9)]
        public void H2_Create_Feliz_JuegoNormal()
        {
            LoginRutaBase();
            Thread.Sleep(2000);
            _driver.FindElement(By.LinkText("Registrar Nuevo Juego")).Click();
            Thread.Sleep(2000);
            _driver.FindElement(By.Id("Titulo")).SendKeys("Minecraft");
            _driver.FindElement(By.Id("Genero")).SendKeys("Supervivencia");
            _driver.FindElement(By.Id("Plataforma")).SendKeys("PC");
            _driver.FindElement(By.Id("Precio")).SendKeys("1500");
            Thread.Sleep(3000);
            _driver.FindElement(By.CssSelector("input[type='submit']")).Click();
            Thread.Sleep(3000);
            TomarCaptura("H2_Feliz");
            Assert.That(_driver.PageSource.Contains("Minecraft"), Is.True);
        }
        #endregion

        #region HISTORIA 4: UPDATE
        [Test, Order(10)]
        public void H4_Update_Negativo_BorrarTodo()
        {
            LoginRutaBase();
            Thread.Sleep(2000);
            var botonesEditar = _driver.FindElements(By.LinkText("Editar"));
            botonesEditar[botonesEditar.Count - 1].Click();
            Thread.Sleep(2000);
            _driver.FindElement(By.Id("Titulo")).Clear();
            Thread.Sleep(3000);
            _driver.FindElement(By.CssSelector("input[type='submit']")).Click();
            Thread.Sleep(3000);
            TomarCaptura("H4_Negativo");
            Assert.That(_driver.Url.Contains("/Edit"), Is.True);
        }

        [Test, Order(11)]
        public void H4_Update_Limite_PrecioCero()
        {
        LoginRutaBase();
        Thread.Sleep(2000);
        var botonesEditar = _driver.FindElements(By.LinkText("Editar"));
    
        // Hacemos clic en el último botón de editar de la lista
        botonesEditar[botonesEditar.Count - 1].Click();
        Thread.Sleep(3000);

        var inputPrecio = _driver.FindElement(By.Id("Precio"));
        inputPrecio.Clear();
    
        // USAMOS UN VALOR NEGATIVO: Esto SIEMPRE debe chocar si el Range empieza en 0 o 1
        inputPrecio.SendKeys("-1.50"); 
        Thread.Sleep(3000);
    
        _driver.FindElement(By.CssSelector("input[type='submit']")).Click();
        Thread.Sleep(4000);
    
        TomarCaptura("H4_Choque_Precio_Negativo");

        Assert.That(_driver.Url.ToLower().Contains("edit"), Is.True, 
        "¡ERROR DE SEGURIDAD! El sistema permitió guardar un precio negativo.");
        }

        [Test, Order(12)]
        public void H4_Update_Feliz_CambiarGenero()
        {
            LoginRutaBase();
            Thread.Sleep(2000);
            var botonesEditar = _driver.FindElements(By.LinkText("Editar"));
            botonesEditar[botonesEditar.Count - 1].Click();
            Thread.Sleep(2000);
            var inputGenero = _driver.FindElement(By.Id("Genero"));
            inputGenero.Clear();
            inputGenero.SendKeys("Aventura");
            Thread.Sleep(3000);
            _driver.FindElement(By.CssSelector("input[type='submit']")).Click();
            Thread.Sleep(3000);
            TomarCaptura("H4_Feliz");
            Assert.That(_driver.PageSource.Contains("Aventura"), Is.True);
        }
        #endregion

        #region HISTORIA 5: DELETE
        [Test, Order(13)]
        public void H5_Delete_Limite_IdInexistente()
        {
            LoginRutaBase();
            Thread.Sleep(2000);
            _driver.Navigate().GoToUrl("http://localhost:5068/Videojuegos/Delete/9999");
            Thread.Sleep(3000);
            TomarCaptura("H5_Limite");
            Assert.That(_driver.PageSource.Contains("Eliminar Juego"), Is.False);
        }

        [Test, Order(14)]
        public void H5_Delete_Negativo_Cancelar()
        {
            LoginRutaBase();
            Thread.Sleep(2000);
            var botonesEliminar = _driver.FindElements(By.LinkText("Eliminar"));
            botonesEliminar[botonesEliminar.Count - 1].Click();
            Thread.Sleep(3000);
            _driver.FindElement(By.LinkText("Cancelar")).Click();
            Thread.Sleep(3000);
            TomarCaptura("H5_Negativo");
            Assert.That(_driver.Url.Contains("/Videojuegos"), Is.True);
        }

        [Test, Order(15)]
        public void H5_Delete_Feliz_Confirmar()
        {
            LoginRutaBase();
            Thread.Sleep(2000);
            var botonesEliminar = _driver.FindElements(By.LinkText("Eliminar"));
            botonesEliminar[botonesEliminar.Count - 1].Click();
            Thread.Sleep(3000);
            _driver.FindElement(By.CssSelector("input[type='submit']")).Click();
            Thread.Sleep(3000);
            TomarCaptura("H5_Feliz");
            Assert.That(_driver.PageSource.Contains("Minecraft"), Is.False);
        }
        #endregion

        private void LoginRutaBase()
        {
            _driver.Navigate().GoToUrl("http://localhost:5068/Account/Login");
            _driver.FindElement(By.Id("Username")).SendKeys("rafael_admin");
            _driver.FindElement(By.Id("Password")).SendKeys("1234");
            _driver.FindElement(By.CssSelector("button[type='submit']")).Click();
            Thread.Sleep(1000);
        }

        [TearDown]
        public void TearDown()
        {
            _driver.Quit();
            _driver.Dispose();
        }
    }
}