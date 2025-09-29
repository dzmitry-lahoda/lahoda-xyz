// Bilingual PT–EN two-column letter (Typst)
#set page(margin: 2.5cm)
#set text(size: 11pt)
#set par(justify: true, leading: 1.3em)


#grid(
  columns: 2,
  rows: 3,
  gutter: 1cm,

  grid.cell(x: 0, y: 0)[
    #text(
      size: 12.5pt,
    )[#strong[Assunto:] Informação da Empresa e Descrição da Atividade — #smallcaps[ABSOLUTOPILAR, LDA]]

    #emph[Exmos. Senhores,]
    #v(6pt)

    Na sequência do vosso pedido, vimos prestar as seguintes informações sobre a sociedade #smallcaps[ABSOLUTOPILAR, LDA].

    - #strong[Denominação:] #smallcaps[ABSOLUTOPILAR, LDA]
    - #strong[Sede registada:] Rua das Pretas, n.º 43, 2.º Andar, Sala 5, 9000-049 Funchal, Portugal
    - #strong[NIPC:] 518 991 148
    - #strong[Vendas mensais esperadas:] 5,000–10,000 EUR

  ],

  grid.cell(x: 1, y: 0)[
    #text(size: 12.5pt)[#strong[Subject:] Company Information and Business Description — #smallcaps[ABSOLUTOPILAR, LDA]]

    #emph[To whom it may concern,]
    #v(6pt)

    In response to your request, we provide the following information regarding #smallcaps[ABSOLUTOPILAR, LDA].

    - #strong[Registered name:] #smallcaps[ABSOLUTOPILAR, LDA]
    - #strong[Registered address:] Rua das Pretas, n.º 43, 2.º Andar, Sala 5, 9000-049 Funchal, Portugal
    - #strong[Tax ID (NIPC):] 518 991 148
    - #strong[Expected monthly sales:] EUR 5,000–10,000
  ],

  grid.cell(x: 0, y: 1)[

    #strong[Serviços de contabilidade]:

    #text(size: 10pt)[#smallcaps[LOVELYWORLD, LDA] (NIPC 514 714 646)]
    #text(size: 10pt)[Morada: Rua das Pretas, n.º 43, 3.º Andar, Sala 9, 9000-049 Funchal, Portugal]

    #v(6pt)
    #block(fill: luma(98%), inset: 8pt, radius: 4pt)[
      #strong[Confirmamos que a informação acima é verdadeira, exata e completa, de acordo com o nosso melhor conhecimento.]
    ]
  ],

  grid.cell(x: 1, y: 1)[

    #strong[Accounting services]:

    #text(size: 10pt)[#smallcaps[LOVELYWORLD, LDA] (NIPC 514 714 646)]
    #text(size: 10pt)[Address: Rua das Pretas, n.º 43, 3.º Andar, Sala 9, 9000-049 Funchal, Portugal]

    #v(6pt)
    #block(fill: luma(98%), inset: 8pt, radius: 4pt)[
      #strong[We confirm that the above information is true, accurate, and complete to the best of our knowledge.]
    ]
  ],

  grid.cell(x: 0, y: 2)[

    #strong[Local e data:] Funchal, ______________________

    #strong[Assinatura do representante:] __________________________

    #strong[Nome e cargo:] ________________________________
  ],

  grid.cell(x: 1, y: 2)[

    #strong[Place and date:] Funchal, ______________________

    #strong[Authorized signatory:] __________________________

    #strong[Name and title:] ________________________________
  ],
)

