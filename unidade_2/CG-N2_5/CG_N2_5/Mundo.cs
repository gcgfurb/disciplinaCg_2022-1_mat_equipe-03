﻿/**
  Autor: Dalton Solano dos Reis
**/

#define CG_Gizmo
// #define CG_Privado

using System;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using System.Collections.Generic;
using OpenTK.Input;
using CG_Biblioteca;

namespace gcgcg
{
  class Mundo : GameWindow
  {
    private static Mundo instanciaMundo = null;

    private Mundo(int width, int height) : base(width, height) { }

    public static Mundo GetInstance(int width, int height)
    {
      if (instanciaMundo == null)
        instanciaMundo = new Mundo(width, height);
      return instanciaMundo;
    }

    private CameraOrtho camera = new CameraOrtho();
    protected List<Objeto> objetosLista = new List<Objeto>();
    private ObjetoGeometria objetoSelecionado = null;
    private char objetoId = '@';
    private bool bBoxDesenhar = false;
    int mouseX, mouseY;   //TODO: achar método MouseDown para não ter variável Global
    private bool mouseMoverPto = false;
    private SegReta obj_Retangulo;
#if CG_Privado
    private Privado_SegReta obj_SegReta;
    private Privado_Circulo obj_Circulo;
#endif



    private Circulo circulo_obj;
    private Circulo circulo_obj2;
    private Circulo circulo_obj3;



    private SegReta obj_SegReta;

    private SegReta obj_SegReta2;
    private SegReta obj_SegReta3;


    Ponto4D ponto0;                     
            
    Ponto4D ponto;

    Ponto4D pontoAux;

  int grau = 45;

int raio = 100;
    protected override void OnLoad(EventArgs e)
    {
      base.OnLoad(e);

     /* Ponto4D ponto0 = new Ponto4D(0, 0);                     
            
      Ponto4D ponto = Matematica.GerarPtosCirculo(grau, raio);

      Ponto4D pontoAux = new Ponto4D(ponto.X + ponto0.X, ponto.Y + ponto0.Y);*/

      ponto0 = new Ponto4D(0, 0);

      ponto = Matematica.GerarPtosCirculo(grau, raio);

     pontoAux = new Ponto4D(ponto.X + ponto0.X, ponto.Y + ponto0.Y);


    
      camera.xmin = -300; camera.xmax = 300; camera.ymin = -300; camera.ymax = 300;

      Console.WriteLine(" --- Ajuda / Teclas: ");
      Console.WriteLine(" [  H     ] mostra teclas usadas. ");

     


      /* objetoId = Utilitario.charProximo(objetoId);
      obj_SegReta3 = new SegReta(objetoId, null, ponto, ponto0);
      obj_SegReta3.ObjetoCor.CorR = 0;
      obj_SegReta3.ObjetoCor.CorG = 50;
      obj_SegReta3.ObjetoCor.CorB = 255;
      objetosLista.Add(obj_SegReta3);
      objetoSelecionado = obj_SegReta3; */






#if CG_Privado
      objetoId = Utilitario.charProximo(objetoId);
      obj_SegReta = new SegReta(objetoId, null, new Ponto4D(50, 150), new Ponto4D(150, 250));
      obj_SegReta.ObjetoCor.CorR = 255; obj_SegReta.ObjetoCor.CorG = 255; obj_SegReta.ObjetoCor.CorB = 0;
      objetosLista.Add(obj_SegReta);
      objetoSelecionado = obj_SegReta;

      objetoId = Utilitario.charProximo(objetoId);
      obj_Circulo = new Privado_Circulo(objetoId, null, new Ponto4D(100, 300), 50);
      obj_Circulo.ObjetoCor.CorR = 0; obj_Circulo.ObjetoCor.CorG = 255; obj_Circulo.ObjetoCor.CorB = 255;
      objetosLista.Add(obj_Circulo);
      objetoSelecionado = obj_Circulo;
#endif
      GL.ClearColor(0.5f, 0.5f, 0.5f, 1.0f);
    }
    protected override void OnUpdateFrame(FrameEventArgs e)
    {

      objetosLista.Clear();                           
            
     ponto = Matematica.GerarPtosCirculo(grau, raio);

     pontoAux = new Ponto4D(ponto.X + ponto0.X, ponto.Y + ponto0.Y);

      objetoId = Utilitario.charProximo(objetoId);
      obj_SegReta3 = new SegReta(objetoId, null, pontoAux, ponto0);
      obj_SegReta3.ObjetoCor.CorR = 0;
      obj_SegReta3.ObjetoCor.CorG = 50;
      obj_SegReta3.ObjetoCor.CorB = 255;

      objetosLista.Add(obj_SegReta3);
      objetoSelecionado = obj_SegReta3;

      base.OnUpdateFrame(e);
      GL.MatrixMode(MatrixMode.Projection);
      GL.LoadIdentity();
      GL.Ortho(camera.xmin, camera.xmax, camera.ymin, camera.ymax, camera.zmin, camera.zmax);

    // Console.WriteLine(ponto.X);

    }
    protected override void OnRenderFrame(FrameEventArgs e)
    {

      
      base.OnRenderFrame(e);
      GL.Clear(ClearBufferMask.ColorBufferBit);
      GL.MatrixMode(MatrixMode.Modelview);
      GL.LoadIdentity();
#if CG_Gizmo      
      Sru3D();
#endif
      for (var i = 0; i < objetosLista.Count; i++)
        objetosLista[i].Desenhar();
      if (bBoxDesenhar && (objetoSelecionado != null))
        objetoSelecionado.BBox.Desenhar();
      this.SwapBuffers();
    }



      private void alteraPonto(int raio, int grau) {

      this.grau = grau;
      this.raio = raio;   
      }

      private void alteraLugar(int x) {

        ponto0.X += x;
        ponto = Matematica.GerarPtosCirculo(grau, raio);
        pontoAux = new Ponto4D(ponto.X + ponto0.X, ponto.Y + ponto0.Y);
        
      

        ponto.X += x;

      }

    protected override void OnKeyDown(OpenTK.Input.KeyboardKeyEventArgs e)
    {
            if (e.Key == Key.H)

            Utilitario.AjudaTeclado();
            else if (e.Key == Key.Escape)
                Exit();
            else if (e.Key == Key.X)
            {


                alteraPonto(raio, grau + 5);

              // grau += 5;
              Console. WriteLine(grau);
                // camera.PanEsquerda();
            }
            else if (e.Key == Key.Z)
            {

                // grau -= 5;
                alteraPonto(raio, grau - 5);

               Console. WriteLine(grau);

                // camera.PanDireita();
            }
            else if (e.Key == Key.Q)
            {



              alteraLugar(-5);
              // ponto.X++;
                // camera.PanCima();
            }

            else if (e.Key == Key.W)
            {

              alteraLugar(+5);

                // ponto0.X++;
              // ponto = Matematica.GerarPtosCirculo(grau, raio);

              // pontoAux = new Ponto4D(ponto.X + ponto0.X, ponto.Y + ponto0.Y);
                

              // heheh

                //camera.PanBaixo();
            }
            else if (e.Key == Key.S)
            {

              raio++;
                // camera.ZoomIn();
            }
            else if (e.Key == Key.A)
            {
                
                raio--;

                // camera.ZoomOut();
            }
            else
                Console.WriteLine(" __ Tecla não implementada.");



  /*    if (e.Key == Key.H)
        Utilitario.AjudaTeclado();
      else if (e.Key == Key.Escape)
        Exit();
      else if (e.Key == Key.E)
      {
        Console.WriteLine("--- Objetos / Pontos: ");
        for (var i = 0; i < objetosLista.Count; i++)
        {
          Console.WriteLine(objetosLista[i]);
        }
      }
      else if (e.Key == Key.O)
        bBoxDesenhar = !bBoxDesenhar;
      else if (e.Key == Key.V)
        mouseMoverPto = !mouseMoverPto;   //TODO: falta atualizar a BBox do objeto
      else
        Console.WriteLine(" __ Tecla não implementada."); */
    }

    //TODO: não está considerando o NDC
    protected override void OnMouseMove(MouseMoveEventArgs e)
    {
      mouseX = e.Position.X; mouseY = 600 - e.Position.Y; // Inverti eixo Y
      if (mouseMoverPto && (objetoSelecionado != null))
      {
        objetoSelecionado.PontosUltimo().X = mouseX;
        objetoSelecionado.PontosUltimo().Y = mouseY;
      }
    }

#if CG_Gizmo
    private void Sru3D()
    {
      GL.LineWidth(1);
      GL.Begin(PrimitiveType.Lines);
      // GL.Color3(1.0f,0.0f,0.0f);
      GL.Color3(Convert.ToByte(255), Convert.ToByte(0), Convert.ToByte(0));
      GL.Vertex3(0, 0, 0); GL.Vertex3(200, 0, 0);
      // GL.Color3(0.0f,1.0f,0.0f);
      GL.Color3(Convert.ToByte(0), Convert.ToByte(255), Convert.ToByte(0));
      GL.Vertex3(0, 0, 0); GL.Vertex3(0, 200, 0);
      // GL.Color3(0.0f,0.0f,1.0f);
      GL.Color3(Convert.ToByte(0), Convert.ToByte(0), Convert.ToByte(255));
      GL.Vertex3(0, 0, 0); GL.Vertex3(0, 0, 200);
      GL.End();
    }
#endif    
  }






  class Program
  {
    static void Main(string[] args)
    {
      Mundo window = Mundo.GetInstance(600, 600);
      window.Title = "CG_N2";
      window.Run(1.0 / 60.0);
    }
  }
}
