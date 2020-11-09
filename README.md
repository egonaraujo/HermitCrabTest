# HermitCrabTest
Teste desenvolvido como parte do processo seletivo para a Hermit Crab Game Studio http://www.hermitcrabstudio.com/


# Ideia principal

Um jogo plataforma com um nivel não linear (não sera somente horizontal ou vertical, mas sim uma área maior que a tela).

- Caixas podem ser movidas
- Arbustos são apenas visuais
- Árvores podem ser usadas como plataforma (tronco não é sólido, folhas são)
- Setas podem apontar para o caminho errado (cor levemente alterada)
- Pedra e Toco de árvore são sólidos
- Cogumelos rosas são necessários para ganhar o nível (coletou X, ganhou)
- X = 5
- Cogumelos laranja são morte certa
- Vãos no chão e água tambem são morte certa

# Mecânicas

As mecânicas desse jogo serão:

- Movimentaçao lateral do jogador
  - Se segurado por mais de 1.5 segundos, o jogador corre
- Pulo
- Pulo duplo? Depois de N cogumelos coletados (N = 3)
- Coletar cogumelos rosas
- Interação de morte com cogumelos laranjas, água, ou vãos

# Interface Gráfica

Toda a movimentação será feita com botões na tela

- Setas para a esquerda e direita para movimentar o jogador, posicionadas no canto inferior esquerdo
- Botão de Pulo, posicionado no canto inferior direito
  - Desativa quando não puder pular (no ar e sem pulo duplo, ou depois do pulo duplo ser usado)
- Contador de cogumelos, no estilo 0 / N, no canto superior esquerdo
- Tempo, no canto superior direito

# Fluxo de jogo

Aqui segue uma descrição do fluxo do jogo e principais ações que devem acontecer

- Abre o menu inicial com 3 botões:
  - Jogar: Inicia um novo jogo (confira passo 2)
  - Recorde: Mostra uma janela com o menor tempo que aquele jogador levou para completar o nivel
  - "Simbolo de auto falante": Pode deixar o jogo mudo ou com som, imagem muda de acordo
- Jogo começa, gera uma entrada no Analytics no evento JogoIniciado, e muda a cena para o jogo
- Cada cogumelo coletado gera uma entrada no Analytics no evento Coleta com a tupla ( CogumeloN , tempo)
- Cada morte gera uma entrada no Analytics no evento Morte com o dado (tempo)
  - Em caso de morte, mostra um anúncio e retorna à tela inicial
- Com N cogumelos coletados, mostra uma pop-up de "pulo duplo habilitado" (Não precisa analytics pois seria o mesmo que Coleta ( CogumeloN , tempo ) )
- Com toods os X cogumelos coletados, mostra uma pop-up de vitória, com o tempo que o usuário levou e um botão de confirmação
  - No clique do botão, atualiza o recorde atual (se necessário), mostra um anúncio e retorna à tela inicial

# Créditos

- Seta utilizada nos botões baixada do Flaticon, do autor [Freepik](https://www.flaticon.com/authors/freepik)
