using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kitware.VTK;

namespace TransformTest
{
    class Program
    {
        static void Main(string[] args)
        {
            TransformTest1();
        }
        public static void TransformTest1()
        {
            vtkSTLReader pSTLReader = vtkSTLReader.New();
            pSTLReader.SetFileName("../../../../res/cow.stl");
            pSTLReader.Update();

            vtkTransform pTransform = vtkTransform.New();
            pTransform.Translate(10.0, 0.0, 0.0);
            pTransform.RotateWXYZ(30, 0.0, 1.0, 0.0);

            vtkTransformPolyDataFilter pTransformFilter = vtkTransformPolyDataFilter.New();
            pTransformFilter.SetInputConnection(pSTLReader.GetOutputPort());
            pTransformFilter.SetTransform(pTransform);
            pTransformFilter.Update();

            vtkPolyDataMapper mapper = vtkPolyDataMapper.New();
            mapper.SetInputConnection(pTransformFilter.GetOutputPort());

            vtkActor actor = vtkActor.New();
            actor.SetMapper(mapper);

            vtkRenderer renderer = vtkRenderer.New();
            renderer.AddActor(actor);
            renderer.SetBackground(.1, .2, .3);
            renderer.ResetCamera();

            vtkRenderWindow renderWin = vtkRenderWindow.New();
            renderWin.AddRenderer(renderer);

            vtkRenderWindowInteractor interactor = vtkRenderWindowInteractor.New();
            interactor.SetRenderWindow(renderWin);

            renderWin.Render();
            interactor.Start();

        }
    }
}
