# Teste Bugaboo Studio (Unity Frontend)

Este projeto é parte de um teste para a empresa Bugaboo Studio que engloba outro projeto de uma API REST feita em Node.js desenvolvidos por Daniel Carlos.
<br>
Esete projeto trata-se de um jogo multiplayer para até dois jogadores cujo objetivo é coletar objetos que surgem aleatoriamente no cenário.

Para acessar o repositório da API, clique <a href="https://github.com/daniel-carlos/bugaboo-test-backend" target="_blank">AQUI</a>

## Requerimentos

Este projeto requer a engine <b>Unity 2023.1.0b19</b> ou superior.
<br>

<span style="font-size: 90%; background-color: gray; color: black">Obs.: a escolha dessa versão se deu por conta do pacote Multiplayer PlayMode que serve para testar multiplayer sem ter que gerar um executável do jogo, mas que só funciona na versão 2023 da Unity. Porém, por conta de um <a href="https://issuetracker.unity3d.com/issues/ui-component-detection-areas-are-offset-in-multiplayer-play-mode-players" target="_blank">bug no pacote</a> que inviabilizaram a sua utilização, optei por usar o ParrelSync que cumpre o mesmo papel.
</span><br>

O jogo também depende do <a href="https://github.com/daniel-carlos/bugaboo-test-backend" target="_blank">backend</a> para funcionar, portanto, é estritamente necessário que sejam seguidos os passos para rodar a API backend no endereço <code>http://localhost:3003</code>
Se for necessário usar outra porta, modifique as configurações no arquivo <code>Assets/Game/API/BackendAPI.cs</code>

## Instalação

1. Baixe e instale a versão <b>2023.1.0b19</b> da Unity através do <b>UnityHub</b>
1. Em uma pasta de sua preferência, faça um clone deste reposiório
    ```console
   git clone https://github.com/daniel-carlos/bugaboo-test-unity
   ```


1. Abra o projeto recém clonado na Unity



1. Dentro da Unity, vá em <b>File > Build and Run<b> e escolha a pasta onde quer que seja criada a build do projeto.
<br>
O executável do projeto será automaticamente aberto e a pasta escolhida para a build será aberta no explorer

1. Abra múltiplas instâncias do executável e jogue
