using WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace WebApp.Controllers
{
    public class AboutController : Controller
    {
        private readonly ILogger<AboutController> _logger;

        public AboutController(ILogger<AboutController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            ViewBag.Title = "Maze Code";
            ViewBag.Texto = "O desafio de programação que mais marcou minha jornada até o momento foi o meu segundo projeto de iniciação na faculdade, onde tivemos a oportunidade de criar um jogo do início ao fim para ensinar programação aos calouros do curso com o nome de MazeCode. Utilizamos tecnologias como C# e Unity, e eu fiquei responsável pela criação da história e diálogo, dos sons e das músicas, além do desenvolvimento do código.\r\n\r\nEssa experiência foi marcante para mim porque sempre fui um grande admirador de jogos, e ter a oportunidade de desenvolver um jogo do zero foi algo que me deixou muito animado e realizado. Além disso, poder usar esse jogo para ajudar outros alunos a aprender programação foi uma experiência incrível e muito gratificante. Essa experiência foi crucial para o desenvolvimento da minha habilidade de trabalho em equipe, além de me ajudar a superar o medo de encarar grandes desafios. Foi incrível poder crescer e aprender junto com os outros alunos envolvidos no projeto, e ter a oportunidade de contribuir para o aprendizado deles também foi muito gratificante.";
            ViewBag.TextoArtigo = "Abaixo deixo o link do nosso artigo científico que publicamos na revista SBGames:";
            ViewBag.NomeArtigo = "Maze Code: Retorica Procedural Aplicada ao Ensino de Logica de Programação";
            ViewBag.Link = "https://www.sbgames.org/proceedings2021/EducacaoFull/218089.pdf";
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
