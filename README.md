BTGPactualBrowniano.app tem o propósito de demonstrar habilidades técnicas.

App desenvolvido para a plataforma Windows.

## 🛠️ Tecnologias utilizadas
- [.NET MAUI](https://learn.microsoft.com/dotnet/maui/)
- C#
- XAML
- Pacote nuget CommunityToolkit.Maui(https://github.com/CommunityToolkit/Maui)
- Pacote nuget CommunityToolkit.Mvvm(https://github.com/CommunityToolkit/dotnet)
- Pacote nuget UraniumUi para interface mais moderna(basicamente utilizado somente o componente dropdown - https://enisn-projects.io/docs/en/uranium/latest/Getting-Started)
- GraphicsView/IDrawable para montar o gráfico

## 🚀 Funcionalidades
- 📊 Gráfico com estilos personalizados:
  - Escolha de linhas
  - Escolha de cores
  - Multiplos resultados
- 🎨 Interface moderna com layout responsivo

## 📂 Estrutura do Projeto
/Models          # Classes de dados (ex: Cores, DadosBrowniano)
/Views           # Telas XAML (SimularVariacaoPrecoView, ColorPickerPopup, CustomEntry)
/ViewModels      # Lógica das telas (SimularVariacaoPrecoViewModel, ColorPickerViewModel)
/Renderers       # Classe que renderiza o gráfico
/Utils           # Métodos auxiliares e enums
README.md        # Este arquivo

🧪 Como rodar o app
Clone o repositório:
git clone https://github.com/seu-usuario/seu-repositorio.git
Abra no Visual Studio 2022+ com suporte ao .NET MAUI.

Compile e execute para Windows:
✅ Defina o projeto como Startup Project
✅ Selecione o emulador ou dispositivo
▶️ Execute (Ctrl + F5)

📸 Capturas de Tela
![Screenshot_4](https://github.com/user-attachments/assets/1fda24f5-6b09-4adc-9cbe-e1b9c9d1a37e)
![Screenshot_3](https://github.com/user-attachments/assets/3a6b751a-33d9-4c98-88cd-7e43f6887966)
![Screenshot_2](https://github.com/user-attachments/assets/4e56dad6-e7a8-41c6-9134-57037097c585)
![Screenshot_1](https://github.com/user-attachments/assets/0a816d1a-7376-4dc0-851a-0d990261bce6)
