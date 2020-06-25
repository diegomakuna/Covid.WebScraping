import * as React from 'react';
import  '../style/index.scss';



export default class Layout extends React.Component {
  public render() {
    return (
      <div className="main-container">
        <div className="sidenav">
          <div className="logo">
            <h1>COVID-19</h1>
            <span>RANKING</span>
          </div>
          <div className='global-data'>
            <h2 className="update-date">20/06/2020</h2>
            <h2 className="type-data confirmed">
            <div className="data">
              <div className="label">Total</div>
              <div className="number color-confirmed">8.546.919</div>
              </div>
            </h2>
            <h2 className="type-data">
              <div className="data">
                <div className="label">Ativos</div>
                <div className="number">3.894.919</div>
              </div>
              <div className="color bg-color-active"></div>
            </h2>
            <h2 className="type-data">
            <div className="data">
              <div className="label">Recuperados</div>
              <div className="number">4.195.274</div>
              </div>
              <div className="color bg-color-recovered"></div>
            </h2>
            <h2 className="type-data">
            <div className="data">
              <div className="label">Fatais</div>
              <div className="number">456.726</div>
              </div>
              <div className="color bg-color-deaths"></div>
            </h2>
          </div>
          <hr className="spacer"/>

          <div className="area-container">
            <div className="filter">
            <input type="text" />
            </div>
            <ul className="list-areas">
            <li className="area">
                Estados unidos
              </li>
              <li className="area">
                brasil
              </li>  <li className="area">
                Estados unidos
              </li>
              <li className="area">
                brasil
              </li>  <li className="area">
                Estados unidos
              </li>
              <li className="area">
                brasil
              </li>  <li className="area">
                Estados unidos
              </li>
              <li className="area">
                brasil
              </li>  <li className="area">
                Estados unidos
              </li>
              <li className="area">
                brasil
              </li>  <li className="area">
                Estados unidos
              </li>
              <li className="area">
                brasil
              </li>  <li className="area">
                Estados unidos
              </li>
              <li className="area">
                brasil
              </li>  <li className="area">
                Estados unidos
              </li>
              <li className="area">
                brasil
              </li>  <li className="area">
                Estados unidos
              </li>
              <li className="area">
                brasil
              </li>  <li className="area">
                Estados unidos
              </li>
              <li className="area">
                brasil
              </li>  <li className="area">
                Estados unidos
              </li>
              <li className="area">
                brasil
              </li>  <li className="area">
                Estados unidos
              </li>
              <li className="area">
                brasil
              </li>  <li className="area">
                Estados unidos
              </li>
              <li className="area">
                brasil
              </li>  <li className="area">
                Estados unidos
              </li>
              <li className="area">
                brasil
              </li>  <li className="area">
                Estados unidos
              </li>
              <li className="area">
                brasil
              </li>  <li className="area">
                Estados unidos
              </li>
              <li className="area">
                brasil
              </li>  <li className="area">
                Estados unidos
              </li>
              <li className="area">
                brasil
              </li>  <li className="area">
                Estados unidos
              </li>
              <li className="area">
                brasil
              </li>  <li className="area">
                Estados unidos
              </li>
              <li className="area">
                brasil
              </li>  <li className="area">
                Estados unidos
              </li>
              <li className="area">
                brasil
              </li>  <li className="area">
                Estados unidos
              </li>
              <li className="area">
                brasil
              </li>  <li className="area">
                Estados unidos
              </li>
              <li className="area">
                brasil
              </li>

            </ul>
          </div>

        </div>
      </div>
    );
  }
}
