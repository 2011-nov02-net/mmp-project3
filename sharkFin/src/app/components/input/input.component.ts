import { Component, OnInit, OnChanges, Input } from '@angular/core';

@Component({
  selector: 'app-input',
  templateUrl: './input.component.html',
  styleUrls: ['./input.component.css']
})
export class InputComponent implements OnInit {

  @Input() type: string = "text";
  @Input() name: string ="";


  constructor() { }

  ngOnInit(): void {
  }


}
