using System.Collections;
using System.IO;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using System;

public class ConfigurationData {
    Dictionary<DataNameEnum, float> confValues = new Dictionary<DataNameEnum, float>();

    #region Properties
    public float ShipThrustForce {
        get { return confValues[DataNameEnum.ShipThrustForce]; }
    }
    /// <summary>
    /// Rotation speed for inputs in degrees per second
    /// </summary>
    public float ShipRotationSpeed {
        get { return confValues[DataNameEnum.ShipRotationSpeed]; }
    }
    public float BulletLifeTime {
        get { return confValues[DataNameEnum.BulletLifeTime]; }
    }
    /// <summary>
    /// Start asteroids' spawn time in seconds
    /// </summary>
    public float AsteroidsSpawnTime {
        get { return confValues[DataNameEnum.AsteroidsSpawnTime]; }
    }
    /// <summary>
    /// Posible deviation of asteroid's movement from straight direction after spawning
    /// </summary>
    public float AsteroidAngleMoveDeviation {
        get { return confValues[DataNameEnum.AsteroidAngleMoveDeviation]; }
    }
    /// <summary>
    /// Minimum start asteroids' magnitude
    /// </summary>
    public float MinAsteroidMagnitude {
        get { return confValues[DataNameEnum.MinAsteroidMagnitude]; }
    }
    /// <summary>
    /// Maximum start asteroids' magnitude
    /// </summary>
    public float MaxAsteroidMagnitude {
        get { return confValues[DataNameEnum.MaxAsteroidMagnitude]; }
    }
    public float DifficultyChangeTime {
        get { return confValues[DataNameEnum.DifficultyChangeTime]; }
    }
    /// <summary>
    /// Amount of asteroids' spawn time decrease after difficulty increase
    /// </summary>
    public float DifficultySpawnTimeMultiplicator {
        get { return confValues[DataNameEnum.DifficultySpawnTimeMultiplicator]; }
    }
    /// <summary>
    /// Amount of asteroids' after spawn speed increase after difficulty increase
    /// </summary>
    public float DifficultyMagnitudeMultiplicator {
        get { return confValues[DataNameEnum.DifficultyMagnitudeMultiplicator]; }
    }
    #endregion
    public ConfigurationData() {
        StreamReader input = null;
        try {
            input = File.OpenText(Path.Combine(Application.streamingAssetsPath, "ConfigurationData.csv"));
            string str_tmp = input.ReadLine();
            while (str_tmp != null) {
                string[] tokens = str_tmp.Split(',');
                DataNameEnum valueName = (DataNameEnum)Enum.Parse(typeof(DataNameEnum), tokens[0]);
                confValues.Add(valueName, float.Parse(tokens[1], System.Globalization.CultureInfo.InvariantCulture));
                str_tmp = input.ReadLine();
            }
        } catch (Exception) {
            SetDefaltValues();
        } finally {
            if (input != null) {
                input.Close();
            }
        }
    }
    void SetDefaltValues() {
        confValues.Clear();
        confValues.Add(DataNameEnum.ShipThrustForce, 10);
        confValues.Add(DataNameEnum.ShipRotationSpeed, 180);
        confValues.Add(DataNameEnum.BulletLifeTime, 2);
        confValues.Add(DataNameEnum.AsteroidsSpawnTime, 5);
        confValues.Add(DataNameEnum.AsteroidAngleMoveDeviation, 30);
        confValues.Add(DataNameEnum.MinAsteroidMagnitude, 2);
        confValues.Add(DataNameEnum.MaxAsteroidMagnitude, 3);
        confValues.Add(DataNameEnum.DifficultyChangeTime, 20);
        confValues.Add(DataNameEnum.DifficultySpawnTimeMultiplicator, 0.2f);
        confValues.Add(DataNameEnum.DifficultyMagnitudeMultiplicator, 0.2f);
    }
}
