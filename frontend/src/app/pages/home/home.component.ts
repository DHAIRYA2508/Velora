import { Component } from '@angular/core';
import { AuthService } from '../../services/auth.service';

@Component({ selector: 'app-home', templateUrl: './home.component.html' })
export class HomeComponent {
  constructor(public auth: AuthService) {}

  experiences = [
    { icon: '✦', title: 'Culinary Artistry', desc: 'Every dish is a canvas. Our chefs paint with the finest seasonal ingredients sourced from around the world.' },
    { icon: '✦', title: 'Sommelier Curated', desc: 'A cellar of over 400 labels, each selected to elevate your dining journey from first course to last.' },
    { icon: '✦', title: 'Private Dining', desc: 'Exclusive rooms for intimate gatherings, corporate events, and celebrations of every kind.' },
  ];
}
