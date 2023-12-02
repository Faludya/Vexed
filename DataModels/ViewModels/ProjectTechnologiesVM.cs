using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModels.ViewModels
{
    public class ProjectTechnologiesVM
    {
        public Project Project { get; set; }
        public List<string> AllTechnologies { get; set; }

        public ProjectTechnologiesVM() { 
            AllTechnologies = GetAllTechnologies();
            Project = new Project();
        }

        public static List<string> GetAllTechnologies()
        {
            return new List<string>()
            {
                "Angular",
                "ASP.NET",
                "AWS",
                "C",
                "C++",
                "C#",
                "Dart",
                "Django",
                "Express.js",
                "Flutter",
                "Git",
                "Go",
                "HTML",
                "Java",
                "JavaScript",
                "jQuery",
                "Kotlin",
                "Laravel",
                "MongoDB",
                "MySQL",
                "Node.js",
                "PHP",
                "PostgreSQL",
                "Python",
                "React",
                "React Native",
                "Ruby",
                "Ruby on Rails",
                "Rust",
                "Sass",
                "Scala",
                "Spring Boot",
                "SQL",
                "Swift",
                "TensorFlow",
                "TypeScript",
                "Unity",
                "Vue.js",
                "ASP.NET Core",
                "Azure",
                "Bootstrap",
                "Docker",
                "Elasticsearch",
                "Firebase",
                "GraphQL",
                "Ionic",
                "jQuery",
                "Kubernetes",
                "Laravel",
                "Machine Learning",
                "Microservices",
                "Next.js",
                "Nginx",
                "PyTorch",
                "Redis",
                "React Router",
                "Redux",
                "RubyMotion",
                "Svelte",
                "Symfony",
                "Terraform",
                "Tizen",
                "Torch",
                "TypeORM",
                "Vagrant",
                "Vapor",
                "Verilog",
                "VHDL",
                "Vue Router",
                "Vuex",
                "WebAssembly",
                "Webpack",
                "WPF",
                "Xamarin",
                "Xcode",
                "XHTML",
                "XML",
                "XSLT",
                "YAML",
                "Yii",
                "Zend Framework",
            };
        }
    }
}
