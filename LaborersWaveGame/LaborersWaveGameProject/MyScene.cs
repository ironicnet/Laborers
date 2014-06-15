// Copyright (C) 2012-2013 Weekend Game Studio
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to
// deal in the Software without restriction, including without limitation the
// rights to use, copy, modify, merge, publish, distribute, sublicense, and/or
// sell copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL
// THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
// FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS
// IN THE SOFTWARE.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WaveEngine.Framework;
using WaveEngine.Components;
using WaveEngine.Common.Math;
using WaveEngine.Framework.Graphics;
using WaveEngine.Materials;
using WaveEngine.Common.Graphics;
using WaveEngine.Framework.Services;
using WaveEngine.Components.Graphics3D;
using Laborers.Tasks;
using Laborers;

namespace LaborersWaveGameProject
{
    public class MyScene : Scene
    {
        private int cubeIndex = 0;
        private Entity singleUnit;

        protected override void CreateScene()
        {

            WaveServices.ScreenContextManager.SetDiagnosticsActive(true);
            Entity isometricCamera = new Entity("isometric")
                                    .AddComponent(new Camera()
                                    {
                                        Position = new Vector3(0, 15f, 15f),
                                        LookAt = Vector3.Zero,
                                    })
                                    .AddComponent(new IsometricCameraBehavior());

            RenderManager.SetActiveCamera(isometricCamera);

            EntityManager.Add(isometricCamera);

            CreateFloor("Floor", Vector3.Zero, new Vector3(25f, 1f, 25f));

            CreateBuilding(new Vector3(5,2,1), new Vector3(1f, 1f, 1f));
            CreateBuilding(new Vector3(-5, 2, 1), new Vector3(1f, 1f, 1f));
            CreateBuilding(new Vector3(-3, 10, 1), new Vector3(1f, 1f, 1f));
            CreateBuilding(new Vector3(25, 3, 5), new Vector3(1f, 1f, 1f));
            CreateBuilding(new Vector3(0, 1, 0), new Vector3(1f, 1f, 1f));


            singleUnit = CreateUnit("Unit 0", new Vector3(0f, 1f, 0f), new Vector3(0.2f, 1, 0.2f)); ;
            

            RenderManager.BackgroundColor = Color.CornflowerBlue;
        }

        protected override void Start()
        {

            IPathResolver pathResolver = new OpenPathResolver();
            UnitBehavior unitBehavior = singleUnit.FindComponent<UnitBehavior>();
            Laborers.UnitWorkPlan workPlan = new Laborers.UnitWorkPlan();
            workPlan.AddTask(new MoveToPositionTask(new Laborers.Position(5, 2, 1), pathResolver));

            workPlan.AddTask(new MoveToPositionTask(new Laborers.Position(-5, 2, 1), pathResolver));

            workPlan.AddTask(new MoveToPositionTask(new Laborers.Position(-3, 10, 3), pathResolver));

            workPlan.AddTask(new MoveToPositionTask(new Laborers.Position(25, 3, 5), pathResolver));

            workPlan.AddTask(new MoveToPositionTask(new Laborers.Position(0, 1, 0), pathResolver));

            unitBehavior.Unit.AssignWorkPlan(workPlan);

            base.Start();
        }
        private Entity CreateBuilding(Vector3 position)
        {
            return CreateBuilding("Cube_" + cubeIndex, position, Vector3.One);
        }

        private Entity CreateBuilding(Vector3 position, Vector3 scale)
        {
            return CreateBuilding("Cube_" + cubeIndex, position, scale);
        }

        private Entity CreateFloor(string cubeName, Vector3 position, Vector3 scale)
        {
            var floor = new Entity(cubeName)
                                  .AddComponent(new Transform3D() { Position = position, Scale = scale })
                                  .AddComponent(Model.CreateCube())
                                  .AddComponent(new MaterialsMap(new BasicMaterial(Color.DarkGreen)))
                                  .AddComponent(new ModelRenderer());


            EntityManager.Add(floor);

            cubeIndex++;
            return floor;
        }
        private Entity CreateBuilding(string cubeName, Vector3 position, Vector3 scale)
        {
            var building = new Entity(cubeName)
                                  .AddComponent(new BuildingBehavior())
                                  .AddComponent(new Transform3D() { Position = position, Scale = scale })
                                  .AddComponent(Model.CreateCube())
                                  .AddComponent(new MaterialsMap(new BasicMaterial(Color.Gray)))
                                  .AddComponent(new ModelRenderer());


            EntityManager.Add(building);

            cubeIndex++;
            return building;
        }
        private Entity CreateUnit(string unitName, Vector3 position, Vector3 scale)
        {
            Color color = GetRandomColor();

            var unitEntity = new Entity(unitName)
                                  .AddComponent(new UnitBehavior())
                                  .AddComponent(new Transform3D() { Position = position, Scale = scale })
                                  .AddComponent(Model.CreateCube())
                                  .AddComponent(new MaterialsMap(new BasicMaterial(color)))
                                  .AddComponent(new ModelRenderer());


            EntityManager.Add(unitEntity);

            cubeIndex++;
            return unitEntity;
        }

        private static Color GetRandomColor()
        {
            var random = WaveServices.Random;
            return new Color((float)random.NextDouble(), (float)random.NextDouble(), (float)random.NextDouble(), 1f);
        }
    }
}
