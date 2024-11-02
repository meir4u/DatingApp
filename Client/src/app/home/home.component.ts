import { DOCUMENT } from '@angular/common';
import { Component, InjectionToken, OnInit, Renderer2, Inject } from '@angular/core';
import { Title, Meta } from '@angular/platform-browser';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {
  registerMode = false;
  users: any;

  constructor(
    private titleService: Title,
    private metaService: Meta,
    private renderer: Renderer2,
    @Inject(DOCUMENT) private document: Document
  ) { }

  ngOnInit(): void {

    this.addTitle();
    this.addTags();  
    this.addPlausibleScript();
  }

  private addTitle(): void {
    // Set the title of the page
    this.titleService.setTitle('Welcome to DatingApp');
  }

  private addTags(): void {
    // Add or update meta tags
    this.metaService.addTags([
      { name: 'description', content: 'This is a description of DatingApp' },
      { name: 'author', content: 'Meir Achildiev' },
      { name: 'keywords', content: 'dating, social, app' }
    ]);
  }

  private addPlausibleScript(): void {
    const script = this.renderer.createElement('script');
    script.type = 'text/javascript';
    script.defer = true;
    script.setAttribute('data-domain', 'dattingapp-app.piemei.easypanel.host');
    script.src = 'https://dattingapp-plausible.piemei.easypanel.host/js/script.js';

    this.renderer.appendChild(this.document.head, script);
  }


  registerToggle() {
    this.registerMode = !this.registerMode;
  }

  cancelRegisterMode(event: boolean) {
    this.registerMode = event;
  }
}

