function addUIKitBlock(blockManager) {
  blockManager.add('h1-block', {
    label: 'According',
    content: `
        <div >
            <ul uk-accordion class="uk-accordion">
                <li>
                    <a class="uk-accordion-title" href="#">Item 1</a>
                    <div class="uk-accordion-content">
                        <p>Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.</p>
                    </div>
                </li>
            </ul>
        </div> 
        `,
    category: 'UIKit',
    attributes: {
      title: 'Insert h1 block'
    }
  });
  blockManager.add('uk-alert', {
    label: 'Alert',
    content: `
    <div class="uk-alert-primary" uk-alert="uk-alert">
        <a class="uk-alert-close" uk-close></a>
        <p>Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt.</p>
    </div> 
        `,
    category: 'UIKit',
    attributes: {
      title: 'Alert' 
    }
  });

  blockManager.add('uk-broadcast',{
      label:'Broadcast',
      content:`
        <ul class="uk-breadcrumb">
            <li class="cell"><a href="#">Item</a></li>
            <li class="cell"><a href="#">Item</a></li>  
        </ul>
    `
  })

  blockManager.add('uk-card',{
      label:'Card',
      content:`
      <div class="uk-card uk-card-default uk-card-body  gjs-cell">
        <h3 class="uk-card-title">Default</h3>
        <p>Lorem ipsum <a href="#">dolor</a> sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.</p>
      </div>
      `
  })

  blockManager.add('uk-button',{
       label:'Button',
       content:`
            <button class="uk-button uk-button-default">Button</button> 
            ` 
  })

  blockManager.add('uk-cover',{
      label:'Video-Cover',
      content:`
      <div class="uk-cover-container uk-height-medium">
            <iframe src="https://www.youtube-nocookie.com/embed/c2pz2mlSfXA?autoplay=1&amp;controls=0&amp;showinfo=0&amp;rel=0&amp;loop=1&amp;modestbranding=1&amp;wmode=transparent" width="1920" height="1080" frameborder="0" allowfullscreen uk-cover></iframe>
        </div>
      `
  })
}
