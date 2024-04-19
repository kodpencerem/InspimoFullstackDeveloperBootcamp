import { AfterViewInit, Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
declare const Chart:any;

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent implements AfterViewInit {
  line:any;
  numbers: number[] = [];
  labels: number[] = [];
  backgroundColor: string[] = [];
  x:number = 0;

  ngAfterViewInit(): void {
    this.showChart();
  }

  constructor(){
    setInterval(()=> {
      this.changeNumber();
    },1000)
  }

  showChart(){
    const ctx = document.getElementById('myChart');

    this.line = new Chart(ctx, {
      type: 'bar',
      data: {
        labels: [],
        datasets: [{
          label: '# of Votes',
          data: [],
          backgroundColor: [],
          borderWidth: 1
        }]
      },
      options: {
        plugins: {
          subtitle: {
              display: true,
              text: 'Custom Chart Subtitle'
          }
      },
        animations: {
          tension: {
            duration: 1000,
            easing: 'linear',
            from: 1,
            to: 0,
            loop: true
          }
        },
        scales: {
          y: {
            beginAtZero: true
          }
        }
      },      
    });
  }

  changeNumber(){
    this.x++;
    this.numbers.push(Math.random() * 30);
    this.labels.push(this.x);
    
    const red = Math.ceil(Math.random() * 255);
    const green = Math.ceil(Math.random() * 255);
    const blue = Math.ceil(Math.random() * 255);
    const color = `rgb(${red},${green},${blue})`

    this.backgroundColor.push(color);

    this.line.data.datasets[0].data = this.numbers;
    this.line.data.datasets[0].backgroundColor = this.backgroundColor;
    this.line.data.labels = this.labels;

    this.line.update();
  }  
}
